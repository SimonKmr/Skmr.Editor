using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Data.Colors
{
    public class Difference<T> : IMultiplyOperators<Difference<T>, float, Difference<T>>
    {
        public Difference(float[] values)
        {
            Values = values;
        }
        public float[] Values { get; set; }

        public static Difference<T> operator *(Difference<T> left, float right)
        {
            float[] res = new float[left.Values.Length];
            
            for (int i = 0; i < left.Values.Length; i++)
            {
                res[i] = left.Values[i] * right;
            }

            return new Difference<T>(res);
        }
    }
}
