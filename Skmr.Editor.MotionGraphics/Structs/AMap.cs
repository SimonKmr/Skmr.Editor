using SkiaSharp;
using Skmr.Editor.Data.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Structs
{
    public struct AMap :
            ISubtractionOperators<AMap, AMap, Difference<AMap>>,
            IMultiplyOperators<Difference<AMap>, float, Difference<AMap>>,
            IAdditionOperators<AMap, Difference<AMap>, AMap>
    {
        public float[,] value;

        public AMap(float[,] value)
        {
            this.value = value;
        }

        public static AMap operator +(AMap left, Difference<AMap> right)
        {
            var width = left.value.GetLength(0);
            var height = left.value.GetLength(1);
            float[,] result = new float[width, height ];
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    result[x,y] = left.value[x, y] + right.Values[y * width + x];
                }
            }

            return new AMap()
            {
                value = result,
            };
        }

        public static Difference<AMap> operator -(AMap left, AMap right)
        {
            var width = left.value.GetLength(0);
            var height = left.value.GetLength(1);
            float[] result = new float[width * height];
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    result[y * width + x] = left.value[x, y] + right.value[x,y];
                }
            }
            
            return new Difference<AMap>(result);
        }

        static Difference<AMap> IMultiplyOperators<Difference<AMap>, float, Difference<AMap>>.operator *(Difference<AMap> left, float right)
        {
            var length = left.Values.Length;
            var result = new float[length];
            
            for (int i = 0; i < length; i++)
            {
                result[i] = left.Values[i] * right;
            }

            return new Difference<AMap>(result);
        }
    }
}
