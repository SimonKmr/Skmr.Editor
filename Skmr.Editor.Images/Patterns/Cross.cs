using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Images.Patterns
{
    public class Cross: ICommand
    {
        public SKBitmap Bitmap { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public (byte r, byte g, byte b) Color { get; set; }

        public int TileSize { get; set; }
        public int CrossSize { get; set; }

        public void Draw()
        {
            var canvas = new SKCanvas(Bitmap);
            var color = new SKPaint { Color = new SKColor(Color.r, Color.g, Color.b) };

            int xOffset = Width % TileSize / 2 - TileSize;
            int yOffset = Height % TileSize / 2 - TileSize;

            int crossOffset = Math.Abs((CrossSize - TileSize)) / 2;
            int tileCenter = TileSize / 2;

            for (int x = xOffset; x < Width; x += TileSize)
                for (int y = yOffset; y < Height; y += TileSize)
                {
                    canvas.DrawLine(
                        new SKPoint(x + tileCenter - CrossSize, y + tileCenter),
                        new SKPoint(x + tileCenter + CrossSize, y + tileCenter),
                        color);
                    canvas.DrawLine(
                        new SKPoint(x + tileCenter, y + tileCenter - CrossSize),
                        new SKPoint(x + tileCenter, y + tileCenter + CrossSize),
                        color);
                }

        }
    }
}
