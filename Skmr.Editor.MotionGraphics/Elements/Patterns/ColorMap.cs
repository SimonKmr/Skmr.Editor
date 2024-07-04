using SkiaSharp;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Images.Patterns
{
    public class ColorMap
    {
        public double[,] map;
        public ColorMap(double[,] map)
        {
            this.map = map;
        }
        public IAttribute<RGB> Color1 { get; set; }
        public IAttribute<RGB> Color2 { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public void DrawOn(int frame, SKCanvas canvas)
        {
            SKBitmap bitmap = new SKBitmap(Width, Height);
            for (int x = 0; x < map.GetLength(0); x ++)
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    (int r, int g , int b) colorfactor =
                        (
                            (int)(Color1.GetFrame(frame).r * map[x, y] + Color2.GetFrame(frame).r * (1 - map[x, y])),
                            (int)(Color1.GetFrame(frame).g * map[x, y] + Color2.GetFrame(frame).g * (1 - map[x, y])),
                            (int)(Color1.GetFrame(frame).b * map[x, y] + Color2.GetFrame(frame).b * (1 - map[x, y]))
                        );

                    var color = new SKColor
                        (
                        (byte) colorfactor.r,
                        (byte) colorfactor.g,
                        (byte) colorfactor.b
                        );
                    bitmap.SetPixel(x, y, color);
                }
            canvas.DrawBitmap(bitmap, 0, 0);
        }
    }
}
