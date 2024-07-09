using Skmr.Editor.Data.Colors;
using Skmr.Editor.Data.Interfaces;
using System.Numerics;

namespace Skmr.Editor.Data
{
    public struct Vec2D :
        ISubtractionOperators<Vec2D, Vec2D, Difference<Vec2D>>,
        IMultiplyOperators<Difference<Vec2D>, float, Difference<Vec2D>>,
        IAdditionOperators<Vec2D, Difference<Vec2D>, Vec2D>,
        IDefault<Vec2D>
    {
        public Vec2D(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float x, y;

        public static Difference<Vec2D> operator -(Vec2D left, Vec2D right)
        {
            return new Difference<Vec2D>
                (new float[] { left.x - right.x, left.y - right.y });
        }

        static Difference<Vec2D> IMultiplyOperators<Difference<Vec2D>, float, Difference<Vec2D>>.operator *(Difference<Vec2D> left, float right)
        {
            return new Difference<Vec2D>(new float[] {
                left.Values[0] * right,
                left.Values[1] * right });
        }

        public static Vec2D operator +(Vec2D left, Difference<Vec2D> right)
        {
            return new Vec2D(
                left.x + right.Values[0],
                left.y + right.Values[1]);
        }

        public static Vec2D GetDefault()
        {
            return new Vec2D(0, 0);
        }
    }
}
