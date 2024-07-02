using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Director
{
    public class Timepoints
    {
        public Timepoints(int[] frames)
        {
            Frames = frames;
        }
        public int[] Frames { get; set; }
        public FramePointers ToFramePointers()
        {
            List<FramePointers.FramePointer> directionFrame = new List<FramePointers.FramePointer>();
            int prevTimepoint = 0;
            int startTimepoint = 0;
            int DirectionDuration = 0;
            for (int i = 0; i < Frames.Length; i++)
            {
                var timepoint = Frames[i];

                if (timepoint - prevTimepoint == 1)
                {
                    DirectionDuration++;
                }
                else
                {
                    if (DirectionDuration != 0) directionFrame.Add(new FramePointers.FramePointer(startTimepoint, DirectionDuration));
                    DirectionDuration = 0;
                    startTimepoint = timepoint;
                }

                prevTimepoint = timepoint;
            }
            return new FramePointers(directionFrame.ToArray());
        }
    }
}
