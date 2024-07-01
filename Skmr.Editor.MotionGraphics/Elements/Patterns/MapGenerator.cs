using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Images
{
    public static class MapGenerator
    {
        #region Generation
        public static double[,] FallOff(int width, int height, (int x,int y) dir, bool flipX, bool flipY)
        {
            double[,] result = new double[width, height];

            //Normalized Vector
            double nFactor = Math.Pow(dir.x * dir.x + dir.y * dir.y, 0.5);
            (double x, double y) nDir = ((dir.x / nFactor), (dir.y / nFactor));
            
            //Draw Map
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    result[x, y] = 
                        ((flipX ? (width - x) : x) / (double)width) * nDir.x + 
                        ((flipY ? (height - y) : y) / (double)height) * nDir.y;

            return result;
        }
        //public static double[,] PerlinNoise(int width, int height, double zoom = 0.005)
        //{
        //    double[,] result = new double[width, height];

        //    for (int x = 0; x < width; x++)
        //        for (int y = 0; y < height; y++)
        //        {
        //            result[x, y] = Perlin.OctavePerlin(x * zoom, y * zoom, 0, 8, 1);
        //        }

        //    return result;
        //}
        public static double[,] Solid(int width, int height, double value)
        {
            double[,] result = new double[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    result[x, y] = value;
                }

            return result;
        }
        #endregion
        public static void RotateMatrix(double[,] map)
        {
            int m = map.GetLength(0);
            int n = map.GetLength(1);
            int row = 0, col = 0;
            double prev, curr;

            while (row < m && col < n)
            {

                if (row + 1 == m || col + 1 == n)
                    break;

                // Store the first element of next
                // row, this element will replace
                // first element of current row
                prev = map[row + 1, col];

                // Move elements of first row
                // from the remaining rows
                for (int i = col; i < n; i++)
                {
                    curr = map[row, i];
                    map[row, i] = prev;
                    prev = curr;
                }
                row++;

                // Move elements of last column
                // from the remaining columns
                for (int i = row; i < m; i++)
                {
                    curr = map[i, n - 1];
                    map[i, n - 1] = prev;
                    prev = curr;
                }
                n--;

                // Move elements of last row
                // from the remaining rows
                if (row < m)
                {
                    for (int i = n - 1; i >= col; i--)
                    {
                        curr = map[m - 1, i];
                        map[m - 1, i] = prev;
                        prev = curr;
                    }
                }
                m--;

                // Move elements of first column
                // from the remaining rows
                if (col < n)
                {
                    for (int i = m - 1; i >= row; i--)
                    {
                        curr = map[i, col];
                        map[i, col] = prev;
                        prev = curr;
                    }
                }
                col++;
            }


        }

        #region arithetic Operators
        public static double[,] Add(this double[,] map1, double[,] map2)
        {
            double[,] result = Copy(map1);

            for (int x = 0; x < result.GetLength(0); x++)
                for (int y = 0; y < result.GetLength(1); y++)
                    result[x, y] += map2[x, y];

            return result;
        }
        public static double[,] Subtract(this double[,] map1, double[,] map2)
        {
            double[,] result = Copy(map1);

            for (int x = 0; x < result.GetLength(0); x++)
                for (int y = 0; y < result.GetLength(1); y++)
                    result[x, y] -= map2[x, y];

            return result;
        }
        public static double[,] Multiply(this double[,] map1, double[,] map2)
        {
            double[,] result = Copy(map1);

            for (int x = 0; x < result.GetLength(0); x++)
                for (int y = 0; y < result.GetLength(1); y++)
                    result[x, y] *= map2[x, y];

            return result;
        }
        public static double[,] Divide(this double[,] map1, double[,] map2)
        {
            double[,] result = Copy(map1);

            for (int x = 0; x < result.GetLength(0); x++)
                for (int y = 0; y < result.GetLength(1); y++)
                    result[x, y] /= map2[x, y];

            return result;
        }
        #endregion

        public static double[,] Copy (this double[,] map)
        {
            double[,] result = new double[map.GetLength(0), map.GetLength(1)];
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    result[x, y] = map[x, y];
            return result;
        }

        public static void Normalize(this double[,] map)
        {
            double minValue = double.MaxValue;
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    minValue = Math.Min(minValue, map[x, y]);
                }

            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] -= minValue;

            double maxValue = double.MinValue;
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    maxValue = Math.Max(maxValue, map[x, y]);
                }

            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] /= maxValue;

        }
        public static void Contrast(this double[,] map)
        {
            for (int x = 0;x < map.GetLength(0); x++)
                for(int y = 0; y < map.GetLength(1); y++)
                {
                    double a = 1 / (1 + Math.Pow(Math.E, - (map[x, y]*12-6)));
                    map[x, y] = a;
                }
        }
        public static void Cut(this double[,] map, double min = 0, double max = 1)
        {
            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    double value;
                    
                    if (map[x, y] < min) value = 0;
                    else if(map[x, y] > max) value = 1;
                    else continue;

                    map[x, y] = value;
                }
        }
    }
}
