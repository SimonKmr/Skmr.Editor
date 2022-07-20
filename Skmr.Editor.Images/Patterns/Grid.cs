using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Images.Patterns
{
    public class Grid : ICommand
    {
        public SKBitmap Bitmap { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public (byte r, byte g, byte b) Color { get; set; }

        public int TileSize { get; set; }

        public void Draw()
        {
            var canvas = new SKCanvas(Bitmap);
            var color = new SKPaint { Color = new SKColor(Color.r, Color.g, Color.b) };
            int xOffset = Width % TileSize / 2 + XOffset;
            int yOffset = Height % TileSize / 2 + YOffset;

            for (int i = xOffset; i < Width; i += TileSize )
                canvas.DrawLine(
                    new SKPoint(i,0),
                    new SKPoint(i,Height),
                    color);

            for (int i = yOffset; i < Height; i += TileSize)
                canvas.DrawLine(
                    new SKPoint(0,i),
                    new SKPoint(Width,i),
                    color);
        }
    }
}
