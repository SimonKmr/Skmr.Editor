using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Analyzer.ComputerVision
{
    public class Position
    {
        public string Name { get; set; }
        
        public Point Center { get; set; }
        public Point Top { get; set; }
        public Point Bottom { get; set; }
        public Point Left { get; set; }
        public Point Right { get; set; }
        
        public int Length { get => Top.X - Bottom.X; }
        public int Height { get => Right.Y - Left.Y;}

        public static Position Create(string name, Point[] contour)
        {
            var position = new Position();


            var pAvr = new Point(0, 0);
            var pMinX = new Point(int.MaxValue, 0);
            var pMinY = new Point(0, int.MaxValue);
            var pMaxX = new Point(0, 0);
            var pMaxY = new Point(0, 0);
            for (int j = 0; j < contour.Length; j++)
            {
                pAvr.X += contour[j].X;
                pAvr.Y += contour[j].Y;

                if (pMinX.X > contour[j].X) pMinX = contour[j];
                if (pMinY.Y > contour[j].Y) pMinY = contour[j];
                if (pMaxX.X < contour[j].X) pMaxX = contour[j];
                if (pMaxY.Y < contour[j].Y) pMaxY = contour[j];
            }
            pAvr.X /= contour.Length;
            pAvr.Y /= contour.Length;

            position.Name = name;
            position.Center = pAvr;
            position.Top = pMaxX;
            position.Bottom = pMinX;
            position.Left = pMinY;
            position.Right = pMaxY;
            return position;
        }
    }
}
