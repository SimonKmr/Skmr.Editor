using Skmr.Editor.Data.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Data
{
    public struct Vec3D :
            ISubtractionOperators<Vec3D, Vec3D, Difference<Vec3D>>,
            IMultiplyOperators<Difference<Vec3D>, float, Difference<Vec3D>>,
            IAdditionOperators<Vec3D, Difference<Vec3D>, Vec3D>
    {
        public Vec3D(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float x, y, z;

        public static Difference<Vec3D> operator -(Vec3D left, Vec3D right)
        {
            return new Difference<Vec3D> 
                (new float[] { left.x - right.x, left.y - right.y });
        }

        static Difference<Vec3D> IMultiplyOperators<Difference<Vec3D>, float, Difference<Vec3D>>.operator *(Difference<Vec3D> left, float right)
        {
            return new Difference<Vec3D> (new float[] { 
                left.Values[0] * right, 
                left.Values[1] * right,
                left.Values[2] * right});
        }

        public static Vec3D operator +(Vec3D left, Difference<Vec3D> right)
        {
            return new Vec3D(
                left.x + right.Values[0], 
                left.y + right.Values[1],
                left.z + right.Values[2]);
        }
    }
}
