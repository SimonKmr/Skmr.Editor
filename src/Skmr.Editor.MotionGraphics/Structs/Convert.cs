using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Structs
{
    public static class Convert
    {
        public static SKPoint ToSkPoint(this Vec2D point)
        {
            return new SKPoint(point.x, point.y);
        }
        public static SKColor ToSkColor(this RGBA color)
        {
            return new SKColor(color.r,color.g,color.b,color.a);
        }
    }
}
