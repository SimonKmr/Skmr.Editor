using Newtonsoft.Json;
using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.MotionGraphics.Attributes;
using Skmr.Editor.MotionGraphics.Enums;
using Skmr.Editor.MotionGraphics.Structs;

namespace Skmr.Editor.MotionGraphics.Elements
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Image : IElement
    {
        [JsonProperty] public string ImagePath { get; set; } = String.Empty;
        [JsonProperty] public IAttribute<AByte> Alpha { get; set; }
        [JsonProperty] public IAttribute<Vec2D> Position { get; set; }

        [JsonProperty] public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Right;
        [JsonProperty] public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Bottom;

        public Image()
        {
            Alpha = new InterpolatedAttribute<AByte>();
            Position = new InterpolatedAttribute<Vec2D>();
        }

        public void DrawOn(int frame, SKCanvas canvas)
        {
            var image = SKImage.FromEncodedData(ImagePath);
            var bm = SKBitmap.FromImage(image);
            var paint = new SKPaint();

            var alpha = Alpha.GetFrame(frame).value;
            if (alpha == 0) return;

            Vec2D pos = Position.GetFrame(frame);

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    pos.x -= image.Width;
                    break;
                case HorizontalAlignment.Center:
                    pos.x -= image.Width / 2;
                    break;
            }

            switch (VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    pos.y -= image.Height;
                    break;
                case VerticalAlignment.Center:
                    pos.y -= image.Height / 2;
                    break;
            }

            paint.IsStroke = true;
            if (alpha != 255) paint.Color = new SKColor(0xFF, 0xFF, 0xFF, alpha);

            canvas.DrawBitmap(bm, pos.x, pos.y, paint);
        }
    }
}
