using SkiaSharp;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.Data.Interfaces;
using System.Numerics;

namespace Skmr.Editor.MotionGraphics.Structs
{
    public struct AMap :
        ISubtractionOperators<AMap, AMap, Difference<AMap>>,
        ISubtractionOperators<AMap, AMap, AMap>,
        IMultiplyOperators<Difference<AMap>, float, Difference<AMap>>,
        IAdditionOperators<AMap, Difference<AMap>, AMap>,
        IAdditionOperators<AMap, AMap, AMap>,
        IMultiplyOperators<AMap, float, AMap>,
        IMultiplyOperators<AMap, AMap, AMap>,
        IDivisionOperators<AMap, AMap, AMap>,
        IDivisionOperators<AMap, float, AMap>,
        IDefault<AMap>
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
            float[,] result = new float[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    result[x, y] = left.value[x, y] + right.Values[y * width + x];
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
                    result[y * width + x] = left.value[x, y] - right.value[x, y];
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

        static AMap ISubtractionOperators<AMap, AMap, AMap>.operator -(AMap left, AMap right)
        {
            var width = left.value.GetLength(0);
            var height = left.value.GetLength(1);
            float[,] result = new float[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    result[x, y] = left.value[x, y] - right.value[x, y];
                }
            }

            return new AMap(result);
        }

        public static AMap operator *(AMap left, float right)
        {
            var width = left.value.GetLength(0);
            var height = left.value.GetLength(1);
            var result = new float[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    result[x, y] = left.value[x, y] * right;
                }
            }

            return new AMap(result);
        }
        public static AMap operator *(AMap left, AMap right)
        {
            var width = left.value.GetLength(0);
            var height = left.value.GetLength(1);
            var result = new float[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    result[x, y] = left.value[x, y] * right.value[x, y];
                }
            }

            return new AMap(result);
        }
        public static AMap operator +(AMap left, AMap right)
        {
            var width = left.value.GetLength(0);
            var height = left.value.GetLength(1);
            var result = new float[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    result[x, y] = left.value[x, y] + right.value[x, y];
                }
            }

            return new AMap(result);
        }
        public static AMap operator /(AMap left, AMap right)
        {
            var width = left.value.GetLength(0);
            var height = left.value.GetLength(1);
            var result = new float[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    result[x, y] = left.value[x, y] / right.value[x, y];
                }
            }

            return new AMap(result);
        }
        public static AMap operator /(AMap left, float right)
        {
            var width = left.value.GetLength(0);
            var height = left.value.GetLength(1);
            var result = new float[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    result[x, y] = left.value[x, y] / right;
                }
            }

            return new AMap(result);
        }

        public static AMap FromFile(string path)
        {
            var image = SKImage.FromEncodedData(path);
            var bm = SKBitmap.FromImage(image);
            var map = new float[image.Width, image.Height];
            var max = float.MinValue;

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    bm.GetPixel(x, y).ToHsv(out float h, out float s, out float v);
                    map[x, y] = v / 100f;
                }
            }

            return new AMap(map);
        }

        public AMap Average(AMap other)
        {
            return this * 0.5f + other * 0.5f;
        }

        public static AMap GetDefault()
        {
            return new AMap(new float[,] { });
        }
    }
}
