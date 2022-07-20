using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Director
{
    public partial class Directions
    {
        public Direction[] DirectionFrames { get; }

        public Directions(Direction[] directionFrames)
        {
            DirectionFrames = directionFrames;
        }
        public (TimeSpan,TimeSpan)[] ToTimeSpanTuple()
        {
            (TimeSpan, TimeSpan)[] result = new (TimeSpan, TimeSpan)[DirectionFrames.Length];
            for (int i = 0; i < DirectionFrames.Length; i++)
            {
                result[i] = (DirectionFrames[i].Start,DirectionFrames[i].Duration);
            }
            return result;
        }
    }
}
