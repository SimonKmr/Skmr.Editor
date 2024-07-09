using Skmr.Editor.Data.Interfaces;
using System.Numerics;

namespace Skmr.Editor.Data.Colors
{
    public struct RGBA :
        ISubtractionOperators<RGBA, RGBA, Difference<RGBA>>,
        IMultiplyOperators<Difference<RGBA>, float, Difference<RGBA>>,
        IAdditionOperators<RGBA, Difference<RGBA>, RGBA>,
        IDefault<RGBA>
    {
        public RGBA(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public byte r;
        public byte g;
        public byte b;
        public byte a;

        public static Difference<RGBA> operator -(RGBA left, RGBA right)
        {
            var res = new Difference<RGBA>(
                new float[]
                {
                    left.r - right.r,
                    left.g - right.g,
                    left.b - right.b,
                    left.a - right.a
                });
            return res;
        }

        static Difference<RGBA> IMultiplyOperators<Difference<RGBA>, float, Difference<RGBA>>.operator *(Difference<RGBA> left, float right)
        {
            var res = new Difference<RGBA>(new float[]
            {
                left.Values[0] * right,
                left.Values[1] * right,
                left.Values[2] * right,
                left.Values[3] * right,
            });
            return res;
        }

        public static RGBA operator +(RGBA left, Difference<RGBA> right)
        {
            var res = new RGBA(
                (byte)(left.r + right.Values[0]),
                (byte)(left.g + right.Values[1]),
                (byte)(left.b + right.Values[2]),
                (byte)(left.a + right.Values[3]));
            return res;
        }

        public static RGBA GetDefault()
        {
            return new RGBA(0, 0, 0, 255);
        }
    }
}
