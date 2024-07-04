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
    public struct AByte :
            ISubtractionOperators<AByte, AByte, Difference<AByte>>,
            IMultiplyOperators<Difference<AByte>, float, Difference<AByte>>,
            IAdditionOperators<AByte, Difference<AByte>, AByte>,
        IDefault<AByte>
    {
        public byte value;

        public AByte(byte value)
        {
            this.value = value;
        }

        public static Difference<AByte> operator -(AByte left, AByte right)
        {
            return new Difference<AByte>(new float[]
            {
                left.value - right.value
            });
        }

        static Difference<AByte> IMultiplyOperators<Difference<AByte>, float, Difference<AByte>>.operator *(Difference<AByte> left, float right)
        {
            return new Difference<AByte>(new float[]
            {
                left.Values[0] * right
            });
        }

        public static AByte operator +(AByte left, Difference<AByte> right)
        {
            return new AByte((byte)(left.value + right.Values[0]));
        }

        public static AByte GetDefault()
        {
            return new AByte(0);
        }
    }
}
