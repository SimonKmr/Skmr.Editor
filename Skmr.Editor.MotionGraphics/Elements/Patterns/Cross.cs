using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics.Attributes;
using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Structs;

namespace Skmr.Editor.Images.Patterns
{
    public class Cross : IElement
    {
        public IAttribute<RGBA> Color { get; set; }
        public IAttribute<Vec2D> Resolution { get; set; }
        public IAttribute<Vec2D> Offset { get; set; }

        public IAttribute<AInt> TileSize { get; set; }
        public IAttribute<AInt> CrossSize { get; set; }

        public Cross()
        {
            Color = new InterpolatedAttribute<RGBA>();
            Resolution = new InterpolatedAttribute<Vec2D>();
            Offset = new InterpolatedAttribute<Vec2D>();
            TileSize = new InterpolatedAttribute<AInt>();
            CrossSize = new InterpolatedAttribute<AInt>();
        }

        public void DrawOn(int frame, SKCanvas canvas)
        {
            var paint = new SKPaint();

            var color = Color.GetFrame(frame);
            paint.Color = new SKColor(
                color.r,
                color.g,
                color.b,
                color.a);

            var resolution = Resolution.GetFrame(frame);
            int tileSize = TileSize.GetFrame(frame).value;
            int crossSize = CrossSize.GetFrame(frame).value;

            float width = resolution.x;
            float height = resolution.y;

            int xOffset = (int)(width % tileSize / 2 - tileSize);
            int yOffset = (int)(height % tileSize / 2 - tileSize);

            int crossOffset = Math.Abs(crossSize - tileSize) / 2;
            int tileCenter = tileSize / 2;

            for (int x = xOffset; x < width; x += tileSize)
                for (int y = yOffset; y < height; y += tileSize)
                {
                    canvas.DrawLine(
                        new SKPoint(x + tileCenter - crossSize, y + tileCenter),
                        new SKPoint(x + tileCenter + crossSize, y + tileCenter),
                        paint);
                    canvas.DrawLine(
                        new SKPoint(x + tileCenter, y + tileCenter - crossSize),
                        new SKPoint(x + tileCenter, y + tileCenter + crossSize),
                        paint);
                }

        }
    }
}
