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
            var result = new Image<RGBA>(width, height);

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
