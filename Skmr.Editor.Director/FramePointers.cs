using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Director
{
    public partial class FramePointers
    {
        public FramePointers(FramePointer[] pointers)
        {
            Pointers = pointers;
        }
        public FramePointer[] Pointers { get; }
        public Directions ToDirections()
        {
            List<Directions.Direction> directions = new List<Directions.Direction>();
            for (int i = 0; i < Pointers.Length; i++)
            {
                directions.Add(new Directions.Direction(new TimeSpan(0, 0, Pointers[i].Start), new TimeSpan(0, 0, Pointers[i].Duration)));
            }
            return new Directions(directions.ToArray());
        }
    }
}
