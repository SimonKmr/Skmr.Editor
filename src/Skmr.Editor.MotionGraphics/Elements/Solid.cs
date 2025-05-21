using Newtonsoft.Json;
using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics.Attributes;
using Skmr.Editor.MotionGraphics.Enums;
using Skmr.Editor.MotionGraphics.Structs;

namespace Skmr.Editor.MotionGraphics.Elements
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Solid : IElement
    {
        [JsonProperty] public IAttribute<Vec2D> Position { get; set; }
        [JsonProperty] public IAttribute<Vec2D> Resolution { get; set; }
        [JsonProperty] public IAttribute<RGBA> Color { get; set; }
        [JsonProperty] public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Right;
        [JsonProperty] public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Bottom;

        public void DrawOn(int frame, SKCanvas canvas)
        {
            var pos = Position.GetFrame(frame);
            var res = Resolution.GetFrame(frame);
            var color = Color.GetFrame(frame).ToSkColor();

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

            using var paint = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color = color,
            };
            
            canvas.DrawRect(pos.x, pos.y, res.x, res.y, paint);
            
        }
    }
}
