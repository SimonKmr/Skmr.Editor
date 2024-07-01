using Skmr.Editor.Data.Colors;
using Skmr.Editor.Engine.Data.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Structs
{
    public struct AVec2D :
            ISubtractionOperators<AVec2D, AVec2D, Difference<AVec2D>>,
            IMultiplyOperators<Difference<AVec2D>, float, Difference<AVec2D>>,
            IAdditionOperators<AVec2D, Difference<AVec2D>, AVec2D>
    {
        public AVec2D(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float x, y;

        public static Difference<AVec2D> operator -(AVec2D left, AVec2D right)
        {
            return new Difference<AVec2D> 
                (new float[] { left.x - right.x, left.y - right.y });
        }

        static Difference<AVec2D> IMultiplyOperators<Difference<AVec2D>, float, Difference<AVec2D>>.operator *(Difference<AVec2D> left, float right)
        {
            return new Difference<AVec2D> (new float[] { 
                left.Values[0] * right, 
                left.Values[1] * right });
        }

        public static AVec2D operator +(AVec2D left, Difference<AVec2D> right)
        {
            return new AVec2D(
                left.x + right.Values[0], 
                left.y + right.Values[1]);
        }
    }
}
