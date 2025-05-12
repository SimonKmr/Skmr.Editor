using System.Numerics;

namespace Skmr.Editor.Data.Colors
{
    public struct YCbCr :
            ISubtractionOperators<RGBA, RGBA, Difference<RGBA>>,
            IMultiplyOperators<Difference<RGBA>, float, Difference<RGBA>>,
            IAdditionOperators<RGBA, Difference<RGBA>, RGBA>
    {
        public YCbCr(byte y, byte cb, byte cr)
        {
            this.y = y;
            this.cb = cb;
            this.cr = cr;
        }

        public byte y;
        public byte cb;
        public byte cr;

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
