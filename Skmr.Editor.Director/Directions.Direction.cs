using System;

namespace Skmr.Editor.Director
{
    public partial class Directions
    {
        public class Direction
        {
            public Direction(TimeSpan start, TimeSpan duration)
            {
                Start = start;
                Duration = duration;
            }

            public TimeSpan Start { get; set; }
            public TimeSpan Duration { get; set; }
        }
    }
}
