using Skmr.Editor.Data.Colors;
using Skmr.Editor.Engine.Y4M;

namespace Skmr.Editor.Engine
{
    public static partial class Utility
    {
        static public Frame ToFrame(this Image<RGB> image)
        {
            int width = image.Width;
            int height = image.Height;

            //Size of the Y Cb Cr section
            int ySize = width * height;
            int cbSize = ySize / 4;
            int crSize = ySize / 4;

            //Creates sub arrays for the byte sections of the y-cb-cr values
            byte[] yBytes = new byte[ySize];
            byte[] cbBytes = new byte[cbSize];
            byte[] crBytes = new byte[crSize];

            //Calculate Y Values and write them to the yBytes Array
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var c = image.Get(x, y);
                    yBytes[x + y * width] = c.ToYCbCr().y;
                }
            }

            //Calculate cb/cr Values and write them to the cb-/crBytes Array
            for (int x = 0; x < image.Width / 2; x++)
            {
                for (int y = 0; y < image.Height / 2; y++)
                {
                    var c = image.Get(x * 2, y * 2);
                    var index = x + y * width / 2;
                    var yCbCr = c.ToYCbCr();

                    cbBytes[index] = yCbCr.cb;
                    crBytes[index] = yCbCr.cr;
                }
            }

            byte[] data = new byte[ySize + cbSize + crSize];
            Array.Copy(yBytes, 0, data, 0, ySize);
            Array.Copy(cbBytes, 0, data, ySize, cbSize);
            Array.Copy(cbBytes, 0, data, ySize + cbSize, crSize);

            return new Frame(width, height, data);
        }

        static public Image<RGB> ToImage(this Frame y4m)
        {
            var width = y4m.Width;
            var height = y4m.Height;
            var frame = y4m.GetData();

            //Size of the Y Cb Cr section
            int ySize = width * height;
            int cbSize = ySize / 4;
            int crSize = ySize / 4;

            // Create separate byte arrays for Y, Cb, and Cr components
            byte[] yComponent = new byte[ySize];
            byte[] cbComponent = new byte[cbSize];
            byte[] crComponent = new byte[crSize];

            // Extract the Y, Cb, and Cr components from frameBytes[0]
            Array.Copy(frame, 0, yComponent, 0, ySize);
            Array.Copy(frame, ySize, cbComponent, 0, cbSize);
            Array.Copy(frame, ySize + cbSize, crComponent, 0, crSize);

            byte[,] cbMap = new byte[width / 2, height / 2];
            byte[,] crMap = new byte[width / 2, height / 2];

            for (int x = 0; x < width / 2; x++)
            {
                for (int y = 0; y < height / 2; y++)
                {
                    var index = x + y * (width / 2);
                    cbMap[x / 2, y / 2] = cbComponent[index];
                    crMap[x / 2, y / 2] = crComponent[index];
                }
            }

            var image = new Image<RGB>(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    image.Set(x, y, 
                        new YCbCr(
                            yComponent[x + y * width], 
                            cbMap[x / 4, y / 4], 
                            crMap[x / 4, y / 4]).ToRgb());
                }
            }

            return image;
        }

        public static RGB ToRgb(this YCbCr color)
        {
            double Y = color.y;
            double Cb = color.cb;
            double Cr = color.cr;

            int r = (int)(Y + 1.40200 * (Cr - 0x80));
            int g = (int)(Y - 0.34414 * (Cb - 0x80) - 0.71414 * (Cr - 0x80));
            int b = (int)(Y + 1.77200 * (Cb - 0x80));

            return new RGB(
                (byte) Math.Max(0, Math.Min(255, r)),
                (byte) Math.Max(0, Math.Min(255, g)),
                (byte) Math.Max(0, Math.Min(255, b))
                );
        }
        public static RGB ToRgb(this YUV color)
        {
            // Conversion formula
            double yd = color.y;
            double ud = color.u - 128;
            double vd = color.v - 128;

            var red = (int)Math.Round(yd + 1.13983 * vd);
            var green = (int)Math.Round(yd - 0.39465 * ud - 0.58060 * vd);
            var blue = (int)Math.Round(yd + 2.03211 * ud);

            // Clamp the RGB values to the valid 8-bit range (0-255)
            red = Math.Max(0, Math.Min(255, red));
            green = Math.Max(0, Math.Min(255, green));
            blue = Math.Max(0, Math.Min(255, blue));

            return new RGB
            {
                r = (byte)red,
                g = (byte)green,
                b = (byte)blue,
            };
        }

        public static YCbCr ToYCbCr(this RGB color)
        {
            double R = (double)color.r / 255;
            double G = (double)color.g / 255;
            double B = (double)color.b / 255;

            double Y = 0.299 * R + 0.587 * G + 0.114 * B;
            double Cb = -0.169 * R - 0.331 * G + 0.500 * B;
            double Cr = 0.500 * R - 0.419 * G - 0.081 * B;

            return new YCbCr(
                    (byte) (Y * 255),
                    (byte) ((Cb + 0.5) * 255),
                    (byte) ((Cr + 0.5) * 255)
                );
        }
        public static YCbCr ToYCbCr(this YUV color)
        {
            return new YCbCr
            {
                y = color.y,
                cr = (byte)(color.v - 128),
                cb = (byte)(color.u - 128),
            };
        }

        public static YUV ToYUV(this RGB color)
        {
            // Conversion formula
            double rd = color.r / 255.0;
            double gd = color.g / 255.0;
            double bd = color.b / 255.0;

            var y = (int)Math.Round(0.299 * rd + 0.587 * gd + 0.114 * bd);
            var u = (int)Math.Round(-0.14713 * rd - 0.28886 * gd + 0.436 * bd) + 128;
            var v = (int)Math.Round(0.615 * rd - 0.51498 * gd - 0.10001 * bd) + 128;

            // Clamp the YUV values to the valid 8-bit range
            y = Math.Max(0, Math.Min(255, y));
            u = Math.Max(0, Math.Min(255, u));
            v = Math.Max(0, Math.Min(255, v));

            return new YUV
            {
                y = (byte)y,
                u = (byte)u,
                v = (byte)v,
            };
        }
        public static YUV ToYUV(this YCbCr color)
        {
            double yd = color.y;
            double crd = color.cr - 128;
            double cbd = color.cb - 128;

            var yuvY = (int)Math.Round(yd + 1.402 * crd);
            var yuvU = (int)Math.Round(yd - 0.34414 * cbd - 0.71414 * crd);
            var yuvV = (int)Math.Round(yd + 1.772 * cbd);

            // Clamp the YUV values to the valid 8-bit range
            yuvY = Math.Max(0, Math.Min(255, yuvY));
            yuvU = Math.Max(0, Math.Min(255, yuvU));
            yuvV = Math.Max(0, Math.Min(255, yuvV));

            return new YUV
            {
                y = (byte)yuvY,
                u = (byte)yuvU,
                v = (byte)yuvV,
            };
        }

        public unsafe static T[] Create<T>(T* ptr, long length) where T : unmanaged
        {
            T[] array = new T[length];
            for (int i = 0; i < length; i++)
                array[i] = ptr[i];
            return array;
        }
        public unsafe static byte* ToPointer(this byte[] bytes)
        {
            fixed (byte* ptr = bytes)
            {
                for (int i = 0; i < bytes.Length; i++)
                    ptr[i]++;

                return ptr;
            }
        }
        public static Image<RGBA> RawToImageRGBA(byte[] bytes, int width, int height)
        {
            int i = 0;
            var result = new Image<RGBA>(width,height);
            
            for (int x = 0; x < 1080; x++)
                for (int y = 0; y < 1920; y++)
                {
                    var c = new RGBA(bytes[i + 0], bytes[i + 1], bytes[i + 2], bytes[i + 3]);
                    result.Set(y, x, c);
                    i += 4;
                }
            
            return result;
        }

        public static Image<RGB> RawToImageRGB(byte[] bytes, int width, int height)
        {
            int i = 0;
            var result = new Image<RGB>(width, height);

            for (int x = 0; x < 1080; x++)
                for (int y = 0; y < 1920; y++)
                {
                    var r = bytes[i + 0];
                    var g = bytes[i + 1];
                    var b = bytes[i + 2];
                    
                    var c = new RGB(r, g, b);
                    result.Set(y, x, c);
                    
                    i += 4;
                }

            return result;
        }
    }
}
