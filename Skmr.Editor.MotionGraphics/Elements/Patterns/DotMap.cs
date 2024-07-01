using SkiaSharp;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Patterns
{
    public class DotMap : IElement
    {
        public Attribute<AMap> Map { get; set; } = new Attribute<AMap>();
        public Attribute<RGBA> Color { get; set; } = new Attribute<RGBA>();
        public Attribute<AVec2D> Resolution { get; set; } = new Attribute<AVec2D>();
        public Attribute<AVec2D> MinMaxSize { get; set; } = new Attribute<AVec2D>();
        public Attribute<AInt> Spaceing { get; set; } = new Attribute<AInt>();

        public Func<float, float> DotSizeFunction { get; set; } = Functions.Linear;

        public void DrawOn(int frame, SKCanvas canvas)
        {
            var spaceing = Spaceing.Interpolate(frame).value;

            float xOffset = (Resolution.Interpolate(frame).x % spaceing) / 2;
            float yOffset = (Resolution.Interpolate(frame).y % spaceing) / 2;

            var map = Map.Interpolate(frame).value;
            var color = Color.Interpolate(frame);
            var paint = new SKPaint();

            paint.Color = new SKColor(
                color.r,
                color.g,
                color.b,
                color.a);
            
            for (int x = (int)xOffset; x < map.GetLength(0); x += spaceing)
                for (int y = (int)yOffset; y < map.GetLength(1); y += spaceing)
                {
                    var dotSizePercent = DotSizeFunction((float)map[x, y]);
                    var min = MinMaxSize.Interpolate(frame).x;
                    var max = MinMaxSize.Interpolate(frame).y;
                    var radius = dotSizePercent * max + min;
                        ;
                    
                    canvas.DrawCircle(
                        new SKPoint(x, y),
                        radius,
                        paint);

                }
        }
    }
}
