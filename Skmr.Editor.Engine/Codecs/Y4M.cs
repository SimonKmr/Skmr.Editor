using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Engine.Codecs
{
    public class Y4M
    {
        public string Path { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string ColorSpace { get; private set; }
        public int FrameSize { get => 1920 * 1080 * 3 / 2; }
        public int HeaderSize { get; private set; }

        public Y4M(string path)
        {
            Path = path;
            string header;
            //Read Header
            using (var sr = new StreamReader(path))
            {
                StringBuilder sb = new StringBuilder();
                header = sr.ReadLine();
                HeaderSize = header.Length + 7; //Extra overhead, probably cause of Frame header, have to write a method to parse it too.
            }

            //Parse Header
            Width = 1920;
            Height = 1080;
            ColorSpace = "4:2:0";
        }

        public Image[] ToImageArray()
        {
            List<Image> result = new List<Image>();
            foreach (var b in ToImageLazy())
            {
                result.Add(b);
            }
            return result.ToArray();
        }

        public IEnumerable<Image> ToImageLazy()
        {
            using (Stream source = File.OpenRead(Path))
            {
                source.Position = HeaderSize;
                byte[] buffer = new byte[FrameSize];
                while (source.Read(buffer, 0, buffer.Length) > 0)
                {
                    yield return ToImage(buffer);
                }
            }
        }

        public Image ToImage(byte[] frame)
        {
            //Size of the Y Cb Cr section
            int ySize = Width * Height;
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

            byte[,] cbMap = new byte[Width / 2, Height / 2];
            byte[,] crMap = new byte[Width / 2, Height / 2];

            for (int x = 0; x < Width / 2; x++)
            {
                for (int y = 0; y < Height / 2; y++)
                {
                    var index = x + y * (Width / 2);
                    cbMap[x / 2, y / 2] = cbComponent[index];
                    crMap[x / 2, y / 2] = crComponent[index];
                }
            }

            Image bitmap = new Image(Width, Height);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    // Get the Y, Cb, and Cr values of the pixel
                    byte yValue = yComponent[x + y * Width];
                    byte cbValue = cbMap[x / 4, y / 4]; // Upsample Cb and Cr components
                    byte crValue = crMap[x / 4, y / 4];

                    Color color = RGBFromYCbCr(yValue, cbValue, crValue);

                    bitmap.Set(x, y, (color.R, color.R, color.R));
                }
            }

            return bitmap;
        }

        private static Color RGBFromYCbCr(byte y, byte cb, byte cr)
        {
            double Y = y;
            double Cb = cb;
            double Cr = cr;

            int r = (int)(Y + 1.40200 * (Cr - 0x80));
            int g = (int)(Y - 0.34414 * (Cb - 0x80) - 0.71414 * (Cr - 0x80));
            int b = (int)(Y + 1.77200 * (Cb - 0x80));

            r = Math.Max(0, Math.Min(255, r));
            g = Math.Max(0, Math.Min(255, g));
            b = Math.Max(0, Math.Min(255, b));

            return Color.FromArgb(r, g, b);
        }
    }
}
