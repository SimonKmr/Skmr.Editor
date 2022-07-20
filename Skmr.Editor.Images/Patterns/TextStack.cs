using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Images.Patterns
{
    public class TextStack : ICommand
    {
        public SKBitmap Bitmap { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public (int x, int y) Position = (0, 0);
        public (byte r, byte g ,byte b) Color = (255,255, 255);
        public string Text { get; set; } = string.Empty;
        public string Font { get; set; } = "arial";
        public int FontSize { get; set; } = 12;
        public bool IsOutline { get; set; } = false;
        public int StrokeWidth { get; set; } = 1;


        public int SpaceX { get; set; } = 0;
        public int StackX { get; set; } = 1;

        public int SpaceY { get; set; } = 0;
        public int StackY { get; set; } = 1;

        public void Draw()
        {
            var canvas = new SKCanvas(Bitmap);
            var typeface = SKTypeface.Default;
            var font = new SKFont(typeface, FontSize);
            var paint = new SKPaint(font)
            {
                Style = IsOutline ? SKPaintStyle.Stroke : SKPaintStyle.Fill,
                Color = new SKColor(Color.r, Color.g, Color.b),
                IsAntialias = true,
                StrokeWidth = StrokeWidth,
            };

            for(int x = 0; x < StackX; x ++)
                for(int y = 0; y < StackY; y++)
                {
                    int posX = Position.x + SpaceX * x;
                    int posY = Position.y + SpaceY * y;
                    canvas.DrawText(Text, posX, posY, paint);
                }

        }
    }
}
