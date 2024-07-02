using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Director
{
    public partial class Timeline
    {
        public int[] Frames { get; }

        public Timeline(int[] frames)
        {
            Frames = frames;
        }

        public Score ToTimelineScore()
        {
            double[] score = new double[Frames.Length];
            for (int i = 0; i < Frames.Length; i++)
            {
                score[i] = Convert.ToDouble(Frames[i]);
            }
            return new Score(score);

        }
        public Timepoints ToTimepoints()
        {
            List<int> timepoints = new List<int>();
            for (int i = 0; i < Frames.Length; i++) if (Frames[i] > 0) timepoints.Add(i);
            return new Timepoints(timepoints.ToArray());
        }

    }
}
