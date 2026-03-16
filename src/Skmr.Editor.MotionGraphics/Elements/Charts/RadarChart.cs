using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.MotionGraphics.Structs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Elements.Charts
{
    internal class RadarChart<T> : IElement
    {
        IEnumerable<T> list;
        public int NumberOfProperties { get; private set; }
        public int Size { get; set; } = 10;
        public int Steps { get; set; } = 1;
        public RadarChart(IEnumerable<T> list, int[] indicies)
        {
            this.list = list;
            var props = list.GetType().GetProperties();
        }

        public void DrawOn(int frame, SKCanvas canvas)
        {
            using var paint = new SKPaint();
            paint.Color = new SKColor(0,0,0,0xFF);
            paint.StrokeWidth = 2;
            
            var rI = 360 / NumberOfProperties;

            DrawLinesToEdges(canvas, paint);
            DrawNgon(canvas, paint);
            DrawProperty(canvas, paint);
        }

        private void DrawNgon(SKCanvas canvas,SKPaint paint)
        {
            var rI = 360 / NumberOfProperties;
            for (int i = 1; i <= Steps; i++)
            {
                var r = 0;
                for (int j = 1; j < NumberOfProperties; j++)
                {
                    var s = (Size / Steps) * i;
                    var p0 = (Vec2D.FromRadian(r) * s).ToSkPoint();
                    var p1 = (Vec2D.FromRadian(r + rI) * s).ToSkPoint();
                    canvas.DrawLine(p0, p1, paint);
                    r += rI;
                }
            }
        }

        private void DrawLinesToEdges(SKCanvas canvas, SKPaint paint)
        {
            var rI = 360 / NumberOfProperties;
            var r = 0;
            for (int i = 0; i < NumberOfProperties; i++)
            {
                var p0 = (Vec2D.FromRadian(r) * Size).ToSkPoint();
                var p1 = new Vec2D(0, 0).ToSkPoint();
                canvas.DrawLine(p0, p1, paint);
                r += rI;
            }
        }

        private void DrawProperty(SKCanvas canvas, SKPaint paint)
        {

        }
    }
}
