using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.MotionGraphics.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Elements
{
    public class Line : IElement
    {
        public IAttribute<Vec2D>[] Points { get; set; }
        public void DrawOn(int frame, SKCanvas canvas)
        {
            throw new NotImplementedException();
        }
    }
}
