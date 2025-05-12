using Skmr.Editor.Data.Colors;
using Skmr.Editor.Data.Interfaces;
using System.Numerics;

namespace Skmr.Editor.MotionGraphics.Structs
{
    public struct AFloat :
        ISubtractionOperators<AFloat, AFloat, Difference<AFloat>>,
        IMultiplyOperators<Difference<AFloat>, float, Difference<AFloat>>,
        IAdditionOperators<AFloat, Difference<AFloat>, AFloat>,
        IDefault<AFloat>
    {
        public float value;

        public AFloat(float value)
        {
            this.value = value;
        }

        public static Difference<AFloat> operator -(AFloat left, AFloat right)
        {
            return new Difference<AFloat>(new float[]
            {
                left.value - right.value
            });
        }

        static Difference<AFloat> IMultiplyOperators<Difference<AFloat>, float, Difference<AFloat>>.operator *(Difference<AFloat> left, float right)
        {
            return new Difference<AFloat>(new float[]
            {
                left.Values[0] * right
            });
        }

        public static AFloat operator +(AFloat left, Difference<AFloat> right)
        {
            return new AFloat((float)(left.value + right.Values[0]));
        }

        public static AFloat GetDefault()
        {
            return new AFloat(0);
        }
    }
}
