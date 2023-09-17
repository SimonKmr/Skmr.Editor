using Skmr.Editor.Engine.Y4M;
using System.Drawing;

namespace Skmr.Editor.Engine
{
    public static partial class Utility
    {
        static public Frame ToFrame(this Image image)
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

        static public Image ToImage(this Frame y4m)
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

            Image image = new Image(width, height);

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

        public static Rgb ToRgb(this YCbCr color)
        {
            double Y = color.y;
            double Cb = color.cb;
            double Cr = color.cr;

            int r = (int)(Y + 1.40200 * (Cr - 0x80));
            int g = (int)(Y - 0.34414 * (Cb - 0x80) - 0.71414 * (Cr - 0x80));
            int b = (int)(Y + 1.77200 * (Cb - 0x80));

            return new Rgb(
                (byte) Math.Max(0, Math.Min(255, r)),
                (byte) Math.Max(0, Math.Min(255, g)),
                (byte) Math.Max(0, Math.Min(255, b))
                );
        }

        public static YCbCr ToYCbCr(this Rgb color)
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

        public static Image Open(string Path)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Image> OpenAll(string path)
        {
            throw new NotImplementedException();
        }
    }
}
