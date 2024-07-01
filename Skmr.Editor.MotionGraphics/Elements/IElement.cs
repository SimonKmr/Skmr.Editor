using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
namespace Skmr.Editor.MotionGraphics.Elements
{
    public interface IElement
    {
        public void DrawOn(int frame, SKCanvas canvas);
    }
}
