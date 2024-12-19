using Skmr.Editor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Structs.Noise
{
    internal class Common
    {
        internal static double LinearlyInterpolate(double valueA, double valueB, double t)
        {
            return valueA + t * (valueB - valueA);
        }

        internal static double SmoothToSCurve(double val)
        {
            return val * val * val * (val * (val * 6d - 15d) + 10d);
        }

        internal static double Dot(Vec3D gradient, double x, double y, double z)
        {
            return gradient.x * x + gradient.y * y + gradient.z * z;
        }
    }
}
