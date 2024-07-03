using Skmr.Editor.Data.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics
{
    public class ProcedualAttribute<T> : IAttribute<T>
    {
        public Func<float, T> Generator { get; set; }
        public T GetFrame(int frame)
        {
            return Generator(frame);
        }
    }
}
