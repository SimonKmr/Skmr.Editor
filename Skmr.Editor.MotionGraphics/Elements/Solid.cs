using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics.Attributes;
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

                canvas.DrawRect(pos.x,pos.y,res.x,res.y,paint);
            }
        }
    }
}
