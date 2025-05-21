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
    public class Text : IElement
    {
        [JsonProperty] public string SourceText { get; set; } = String.Empty;
        [JsonProperty] public string FontFile { get; set; } = String.Empty;
        [JsonProperty] public float TextSize { get; set; } = 64.0f;
        [JsonProperty] public bool IsStroke { get; set; } = false;
        [JsonProperty] public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Center;
        [JsonProperty] public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Center;
        [JsonProperty] public IAttribute<Vec2D> Position { get; set; }
        [JsonProperty] public IAttribute<RGBA> Color { get; set; }
        [JsonProperty] public IAttribute<AInt> Rotation { get; set; }
        public LetterAnimation CustomAnimation { get; set; }

        public delegate void LetterAnimation(SKCanvas canvas, Text ctx, int frame);

        private static SKTextAlign ToSKTextAlign(HorizontalAlignment alignment)
        {
            switch (alignment)
            {
                case HorizontalAlignment.Left: return SKTextAlign.Left;
                case HorizontalAlignment.Center: return SKTextAlign.Center;
                case HorizontalAlignment.Right: return SKTextAlign.Right;
                default: throw new Exception();
            }
        }

        public Text()
        {
            Color = new InterpolatedAttribute<RGBA>();
            Position = new InterpolatedAttribute<Vec2D>();
            Rotation = new InterpolatedAttribute<AInt>();
        }

        public void DrawOn(int frame, SKCanvas canvas)
        {
            if (CustomAnimation != null)
            {
                CustomAnimation(canvas, this, frame);
                return;
            }

            using (var paint = new SKPaint())
            {
                //set default values of text

                if (FontFile != String.Empty)
                {
                    paint.Typeface = SKTypeface.FromFile(FontFile);
                }

                paint.TextSize = TextSize;
                paint.IsAntialias = true;
                paint.IsStroke = IsStroke;
                paint.TextAlign = ToSKTextAlign(HorizontalAlignment);
                paint.Color = Color.GetFrame(frame).ToSkColor();

                var pos = Position.GetFrame(frame);

                switch (VerticalAlignment)
                {
                    case VerticalAlignment.Top:
                        pos.y -= (float)(TextSize / 0.75);
                        break;
                    case VerticalAlignment.Center:
                        pos.y -= (float)(TextSize / 0.75) / 2;
                        break;
                }

                var rotation = Rotation.GetFrame(frame).value;

                canvas.Save();
                canvas.RotateDegrees(rotation, pos.x, pos.y);
                canvas.DrawText(
                    SourceText,
                    pos.x,
                    pos.y,
                    paint);
                canvas.Restore();
            }

        }
    }
}
