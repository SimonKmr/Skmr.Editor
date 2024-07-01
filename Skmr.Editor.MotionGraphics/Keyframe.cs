using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics
{
    public class Keyframe<T>
    {
        public T Value { get; set; }
        public int Frame { get; set; }
        public Func<float,float> Transition { get; set; }
    }
}
