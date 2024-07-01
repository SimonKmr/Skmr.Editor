using Skmr.Editor.Data.Colors;
using Skmr.Editor.Engine.Data.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Data
{
    public struct Pos2D :
            ISubtractionOperators<Pos2D, Pos2D, Difference<Pos2D>>,
            IMultiplyOperators<Difference<Pos2D>, float, Difference<Pos2D>>,
            IAdditionOperators<Pos2D, Difference<Pos2D>, Pos2D>
    {
        public Pos2D(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float x, y;

        public static Difference<Pos2D> operator -(Pos2D left, Pos2D right)
        {
            return new Difference<Pos2D> 
                (new float[] { left.x - right.x, left.y - right.y });
        }

        static Difference<Pos2D> IMultiplyOperators<Difference<Pos2D>, float, Difference<Pos2D>>.operator *(Difference<Pos2D> left, float right)
        {
            return new Difference<Pos2D> (new float[] { 
                left.Values[0] * right, 
                left.Values[1] * right });
        }

        public static Pos2D operator +(Pos2D left, Difference<Pos2D> right)
        {
            return new Pos2D(
                left.x + right.Values[0], 
                left.y + right.Values[1]);
        }
    }
}
