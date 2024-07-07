using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics.Attributes;
using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Structs;

namespace Skmr.Editor.MotionGraphics.Patterns
{
    public class DotMap : IElement
    {
        public IAttribute<AMap> Map { get; set; }
        public RGBA ColorMin { get; set; }
        public RGBA ColorMax { get; set; }
        public IAttribute<Vec2D> Resolution { get; set; }
        public IAttribute<Vec2D> MinMaxSize { get; set; }
        public IAttribute<AInt> Spaceing { get; set; }

        public DotMap()
        {
            Map = new InterpolatedAttribute<AMap>();
            Resolution = new InterpolatedAttribute<Vec2D>();
            MinMaxSize = new InterpolatedAttribute<Vec2D>();
            Spaceing = new InterpolatedAttribute<AInt>();
        }

        public Func<float, float> DotSizeFunction { get; set; } = Function.Linear;

        public void DrawOn(int frame, SKCanvas canvas)
        {
            var spaceing = Spaceing.GetFrame(frame).value;
            var resolution = Resolution.GetFrame(frame);

            float xOffset = (resolution.x % spaceing) / 2;
            float yOffset = (resolution.y % spaceing) / 2;

            var map = Map.GetFrame(frame).value;
            ;
            var paint = new SKPaint();


            
            for (int x = (int)xOffset; x < map.GetLength(0); x += spaceing)
                for (int y = (int)yOffset; y < map.GetLength(1); y += spaceing)
                {
                    var color = ColorMin + (ColorMax - ColorMin) * (float)map[x, y];
                    
                    paint.Color = new SKColor(
                        color.r,
                        color.g,
                        color.b,
                        color.a);
                    var dotSizePercent = DotSizeFunction((float)map[x, y]);
                    var minmax = MinMaxSize.GetFrame(frame);
                    var min = minmax.x;
                    var max = minmax.y;
                    var radius = dotSizePercent * max + min;
                    
                    canvas.DrawCircle(
                        new SKPoint(x, y),
                        radius,
                        paint);

                }
        }
    }
}
