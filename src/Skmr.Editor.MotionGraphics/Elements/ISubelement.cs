using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Elements
{
    internal interface ISubelement<T> : IDisposable
    {
        public T GetValue(int frame);
    }
}
