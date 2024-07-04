using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.MotionGraphics.Attributes;
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
        public IAttribute<AByte> Alpha { get; set; }
        public IAttribute<Vec2D> Position { get; set; }

        public Image()
        {
            Alpha = new InterpolatedAttribute<AByte>();
            Position = new InterpolatedAttribute<Vec2D>();
        }

        public void DrawOn(int frame, SKCanvas canvas)
        {
            var image = SKImage.FromEncodedData(ImagePath);
            var bm = SKBitmap.FromImage(image);
            var paint = new SKPaint();

            var alpha = Alpha.GetFrame(frame).value;
            if (alpha == 0) return;

            var pos = Position.GetFrame(frame);

            if (alpha != 255) paint.Color = new SKColor(0, 0, 0, alpha);
            
            canvas.DrawBitmap(bm, pos.x, pos.y, paint);
        }
    }
}
