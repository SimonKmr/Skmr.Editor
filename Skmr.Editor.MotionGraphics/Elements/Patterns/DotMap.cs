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
            float xOffset = (Resolution.Interpolate(frame).x % Spaceing.Interpolate(frame).value) / 2;
            float yOffset = (Resolution.Interpolate(frame).y % Spaceing.Interpolate(frame).value) / 2;

            var map = Map.Interpolate(frame).value;
            var color = Color.Interpolate(frame);
            var paint = new SKPaint();

            paint.Color = new SKColor(
                color.r,
                color.g,
                color.b,
                color.a);
            
            for (int x = (int)xOffset; x < map.GetLength(0); x += Spaceing.Interpolate(frame).value)
                for (int y = (int)yOffset; y < map.GetLength(1); y += Spaceing.Interpolate(frame).value)
                {
                    var radius = DotSizeFunction((float)map[x, y]) * MinMaxSize.Interpolate(frame).x + 
                        MinMaxSize.Interpolate(frame).y;
                    
                    canvas.DrawCircle(
                        new SKPoint(x, y),
                        radius,
                        paint);

                }
        }
    }
}
