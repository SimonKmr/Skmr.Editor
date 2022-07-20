using System;

namespace Skmr.Editor.Director
{
    public class Score
    {
        public double[] Scores { get; private set; }
        public Score(double[] scores)
        {
            Scores = scores;
        }
        public void Smooth(int radius = 15)
        {
            double[] sums = new double[Scores.Length];
            double[] averages = new double[Scores.Length];
            for(int i = 0; i < Scores.Length; i++)
            {
                for(int j = Math.Max(i - radius, 0); 
                    j < Math.Min(i + radius, Scores.Length);
                    j++)
                {
                    sums[i] += Scores[j];
                }
                averages[i] = sums[i] / (double)radius;
            }
            Scores = averages;
        }
        public void Add(double[] addend)
        {
            int I = Math.Min(addend.Length, Scores.Length);
            for (int i = 0; i < I; i++) { Scores[i] += addend[i]; }
        }
        public void Subtract(double[] subtrahend)
        {
            int I = Math.Min(subtrahend.Length, Scores.Length);
            for (int i = 0; i < I; i++) { Scores[i] -= subtrahend[i]; }
        }
        public void Multiply(double[] multiplicand)
        {
            int I = Math.Min(multiplicand.Length, Scores.Length);
            for (int i = 0; i < I; i++) { Scores[i] *= multiplicand[i]; }
        }
        public void Divide(double[] divisor)
        {
            int I = Math.Min(divisor.Length, Scores.Length);
            for (int i = 0; i < I; i++) { Scores[i] -= divisor[i]; }
        }
        
        public Timeline ToTimeline()
        {
            int[] frames = new int[Scores.Length];
            for(int i = 0; i < Scores.Length; i++)
            {
                frames[i] = Convert.ToInt32(Scores[i]);
            }
            return new Timeline(frames);
        }
        public Timeline ToTimeline(double threshold = 1)
        {
            int[] frames = new int[Scores.Length];
            for (int i = 0; i < Scores.Length; i++)
            {
                if (Scores[i] >= threshold) frames[i] = Convert.ToInt32(Math.Ceiling(Scores[i]));
            }
            return new Timeline(frames);
        }
    }
}
