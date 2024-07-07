using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics.Attributes;
using Skmr.Editor.MotionGraphics.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Elements
{
    internal class Solid : IElement
    {
        public IAttribute<Vec2D> Position { get; set; }
        public IAttribute<Vec2D> Resolution { get; set; }
        public IAttribute<RGBA> Color { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }

        public void DrawOn(int frame, SKCanvas canvas)
        {
            using (var paint = new SKPaint()) 
            {
                var pos = Position.GetFrame(frame);
                var res = Resolution.GetFrame(frame);
                var color = Color.GetFrame(frame);

                paint.Color = new SKColor(
                    color.r,
                    color.g,
                    color.b,
                    color.a);

                switch (HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        pos.x -= res.x;
                        break;
                    case HorizontalAlignment.Center:
                        pos.x -= res.x / 2;
                        break;
                }

                switch (VerticalAlignment)
                {
                    case VerticalAlignment.Top:
                        pos.y -= res.y;
                        break;
                    case VerticalAlignment.Center:
                        pos.y -= res.y / 2;
                        break;
                }

                canvas.DrawRect(pos.x,pos.y,res.x,res.y,paint);
            }
        }
    }
}
