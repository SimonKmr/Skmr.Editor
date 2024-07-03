using Skmr.Editor.Data.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics
{
    public interface IAttribute<T>
    {
        public abstract T GetFrame(int frame);
    }
}
