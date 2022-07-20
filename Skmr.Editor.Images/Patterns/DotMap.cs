using SkiaSharp;
using Skmr.Editor.Images.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Images.Patterns
{
    public class DotMap : ICommand
    {


        private double[,] map;
        public DotMap(double[,]map)
        {
            this.map = map;
        }
        public (byte r,byte g, byte b) Color { get; set; }


        public SKBitmap Bitmap { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }


        public int Spaceing { get; set; }
        public int MinSize { get; set; }
        public int MaxSize { get; set; }
        public DotSizeHandler DotSizeFunction { get; set; } = ValueMapping.Linear;

        public void Draw()
        {
            var canvas = new SKCanvas(Bitmap);
            int xOffset = (Width % Spaceing) / 2 ;
            int yOffset = (Height % Spaceing) / 2 ;

            for (int x = xOffset; x < map.GetLength(0); x += Spaceing)
                for(int y = yOffset; y < map.GetLength(1); y += Spaceing)
                {
                    
                    canvas.DrawCircle(
                        new SKPoint(x, y), 
                        DotSizeFunction(MinSize, 
                        MaxSize, 
                        map[x, y])+0.5f,
                        new SKPaint 
                        { 
                            Color = new SKColor(Color.r,Color.g,Color.b)
                        });
                }
        }



        public delegate int DotSizeHandler(int min, int max, double value);
    }
}
