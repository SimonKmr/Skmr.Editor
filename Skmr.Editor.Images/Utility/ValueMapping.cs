using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Images.Utility
{
    public static class ValueMapping
    {
        public static int Linear(int min, int max, double value)
        {
            double s = (max - min) * value;
            int res = (int)Math.Round(min + s);
            return res;
        }

        public static int Sigmoid(int min, int max, double value)
        {
            double a = max / (1 + Math.Pow(Math.E, -value));
            double b = (max - min) * a;
            int res = (int)Math.Round(b);
            return res;
        }

        public static int Binary(int min, int max, double value)
        {
            if (value >= 0.5) return max;
            else return min;
        }
    }
}
