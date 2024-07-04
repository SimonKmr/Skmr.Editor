using Skmr.Editor.Data.Colors;
using Skmr.Editor.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Structs
{
    public struct AInt :
        ISubtractionOperators<AInt, AInt, Difference<AInt>>,
        IMultiplyOperators<Difference<AInt>, float, Difference<AInt>>,
        IAdditionOperators<AInt, Difference<AInt>, AInt>,
        IDefault<AInt>
    {
        public int value;

        public AInt(byte value)
        {
            this.value = value;
        }

        public static Difference<AInt> operator -(AInt left, AInt right)
        {
            return new Difference<AInt>(new float[]
            {
                left.value - right.value
            });
        }

        static Difference<AInt> IMultiplyOperators<Difference<AInt>, float, Difference<AInt>>.operator *(Difference<AInt> left, float right)
        {
            return new Difference<AInt>(new float[]
            {
                left.Values[0] * right
            });
        }

        public static AInt operator +(AInt left, Difference<AInt> right)
        {
            return new AInt((byte)(left.value + right.Values[0]));
        }

        public static AInt GetDefault()
        {
            return new AInt(0);
        }
    }
}
