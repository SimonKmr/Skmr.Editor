using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.MotionGraphics.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Elements
{
    public class Image : IElement
    {
        public string ImagePath { get; set; } = String.Empty;
        public Attribute<AByte> Alpha { get; set; }
        public Attribute<Vec2D> Position { get; set; }

        public Image()
        {
            Alpha = new Attribute<AByte>();
            Position = new Attribute<Vec2D>();
        }

        public void DrawOn(int frame, SKCanvas canvas)
        {
            var image = SKImage.FromEncodedData(ImagePath);
            var bm = SKBitmap.FromImage(image);
            var paint = new SKPaint();

            var alpha = Alpha.Interpolate(frame).value;
            if (alpha == 0) return;

            var pos = Position.Interpolate(frame);

            if (alpha != 255) paint.Color = new SKColor(0, 0, 0, alpha);
            
            canvas.DrawBitmap(bm, pos.x, pos.y, paint);
        }
    }
}
