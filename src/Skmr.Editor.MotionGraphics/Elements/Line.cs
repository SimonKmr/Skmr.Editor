using Newtonsoft.Json;
using Silk.NET.SDL;
using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics.Attributes;
using Skmr.Editor.MotionGraphics.Structs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Elements
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Line : IElement
    {
        [JsonProperty] public IAttribute<Vec2D>[] Points { get; set; }
        [JsonProperty] public IAttribute<AFloat> Start { get; set; }
        [JsonProperty] public IAttribute<AFloat> End { get; set; }
        [JsonProperty] public IAttribute<AFloat> Width { get; set; }
        [JsonProperty] public IAttribute<RGBA> Color { get; set; }

        public Line()
        {
            Points = new InterpolatedAttribute<Vec2D>[0];
            Start = new StaticAttribute<AFloat>(new AFloat(0f));
            End = new StaticAttribute<AFloat>(new AFloat(1f));
        }

        public void DrawOn(int frame, SKCanvas canvas)
        {
            if (Points.Length < 2) return;

            using var paint = new SKPaint();

            var color = Color.GetFrame(frame);
            var width = Width.GetFrame(frame);
            var start = Start.GetFrame(frame).value;
            var end = End.GetFrame(frame).value;

            if (start >= end) return;

            paint.Color = new SKColor(
                color.r,
                color.g,
                color.b,
                color.a);

            paint.StrokeWidth = width.value;

            double totalLength = 0d;

            for (int i = 0; i < Points.Length - 1; i++)
            {
                var p1 = Points[i].GetFrame(frame);
                var p2 = Points[i + 1].GetFrame(frame);

                totalLength += GetLength(p1,p2);
            }

            double p1Length = 0d;
            double p2Length = 0d;

            for (int i = 0; i < Points.Length - 1; i++)
            {
                var p1 = Points[i].GetFrame(frame);
                var p1x = p1.x; 
                var p1y = p1.y;
                
                var p2 = Points[i+1].GetFrame(frame);
                var p2x = p2.x; 
                var p2y = p2.y;

                double currentLength = GetLength(p1, p2) / totalLength;
                p2Length += currentLength;

                if (p1Length > end) return;
                if (p2Length > end)
                {
                    //calculate new p2 Position
                    var d = (end - p1Length) / currentLength;
                    p2x = (float)(p1.x + (p2.x - p1.x) * d);
                    p2y = (float)(p1.y + (p2.y - p1.y) * d);
                }

                if (p1Length < start)
                {
                    var d = (p2Length - start) / currentLength;
                    p1x = (float)(p2.x + (p1.x - p2.x) * d);
                    p1y = (float)(p2.y + (p1.y - p2.y) * d);
                }

                p1Length = p2Length;

                canvas.DrawLine(p1x, p1y, p2x, p2y, paint);
            }
        }

        double GetLength(Vec2D p1, Vec2D p2)
        {
            return Math.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
        }
    }
}
