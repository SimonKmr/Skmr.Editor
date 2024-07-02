using Skmr.Editor.Data.Colors;
using System.Numerics;

namespace Skmr.Editor.Data.Colors
{
    public struct RGB :
            ISubtractionOperators<RGBA, RGBA, Difference<RGBA>>,
            IMultiplyOperators<Difference<RGBA>, float, Difference<RGBA>>,
            IAdditionOperators<RGBA, Difference<RGBA>, RGBA>
    {
        public RGB(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public byte r;
        public byte g;
        public byte b;

        static Difference<RGBA> ISubtractionOperators<RGBA, RGBA, Difference<RGBA>>.operator -(RGBA left, RGBA right)
        {
            throw new NotImplementedException();
        }

        static Difference<RGBA> IMultiplyOperators<Difference<RGBA>, float, Difference<RGBA>>.operator *(Difference<RGBA> left, float right)
        {
            throw new NotImplementedException();
        }

        static RGBA IAdditionOperators<RGBA, Difference<RGBA>, RGBA>.operator +(RGBA left, Difference<RGBA> right)
        {
            throw new NotImplementedException();
        }
    }
}
