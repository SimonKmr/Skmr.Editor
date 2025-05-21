using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics.Attributes;
using Skmr.Editor.MotionGraphics.Enums;
using Skmr.Editor.MotionGraphics.Structs;
using Newtonsoft.Json;

namespace Skmr.Editor.MotionGraphics.Elements
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Gradient : IElement
    {
        [JsonProperty] public IAttribute<Vec2D> Position { get; set; }
        [JsonProperty] public IAttribute<Vec2D> Resolution { get; set; }
        [JsonProperty] public IAttribute<RGBA>[] Colors { get; set; }
        [JsonProperty] public IAttribute<Vec2D> GradientP1 { get; set; }
        [JsonProperty] public IAttribute<Vec2D> GradientP2 { get; set; }
        [JsonProperty] public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Right;
        [JsonProperty] public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Bottom;

        public Gradient()
        {
            Position = new StaticAttribute<Vec2D>();
            Resolution = new StaticAttribute<Vec2D>();
            GradientP1 = Position;
            GradientP2 = Resolution;
        }

        public void DrawOn(int frame, SKCanvas canvas)
        {
            using (var paint = new SKPaint())
            {
                var pos = Position.GetFrame(frame);
                var res = Resolution.GetFrame(frame);
                var p1 = GradientP1.GetFrame(frame).ToSkPoint();
                var p2 = GradientP2.GetFrame(frame).ToSkPoint();

                var colors = Colors.Select(x => x.GetFrame(frame).ToSkColor()).ToArray();
                var shader = SKShader.CreateLinearGradient(p1,p2,colors, SKShaderTileMode.Clamp);

                paint.Style = SKPaintStyle.Fill;
                paint.Shader = shader;

                switch (HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        pos.x -= res.x;
                        break;
                    case HorizontalAlignment.Center:
                        pos.x -= res.x / 2;
                        break;
                }

                switch (VerticalAlignment)
                {
                    case VerticalAlignment.Top:
                        pos.y -= res.y;
                        break;
                    case VerticalAlignment.Center:
                        pos.y -= res.y / 2;
                        break;
                }

                canvas.DrawRect(pos.x, pos.y, res.x, res.y, paint);
            }
        }
    }
}
