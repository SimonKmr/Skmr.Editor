using System.Numerics;

namespace Skmr.Editor.Data.Colors
{
    public struct YUV :
            ISubtractionOperators<RGBA, RGBA, Difference<RGBA>>,
            IMultiplyOperators<Difference<RGBA>, float, Difference<RGBA>>,
            IAdditionOperators<RGBA, Difference<RGBA>, RGBA>
    {
        public YUV(byte y, byte u, byte v)
        {
            this.y = y;
            this.u = u;
            this.v = v;
        }

        public byte y;
        public byte u;
        public byte v;

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
