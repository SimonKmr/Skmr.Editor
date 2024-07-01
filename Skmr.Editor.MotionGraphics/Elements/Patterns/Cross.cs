using SkiaSharp;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics;
using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Images.Patterns
{
    public class Cross: IElement
    {
        public Attribute<RGBA> Color { get; set; } = new Attribute<RGBA>();
        public Attribute<AVec2D> Resolution { get; set; } = new Attribute<AVec2D>();
        public Attribute<AVec2D> Offset { get; set; } = new Attribute<AVec2D>();

        public Attribute<AInt> TileSize { get; set; } = new Attribute<AInt>();
        public Attribute<AInt> CrossSize { get; set; } = new Attribute<AInt>();

        public void DrawOn(int frame, SKCanvas canvas)
        {
            var paint = new SKPaint();
            paint.Color = new SKColor(
                Color.Interpolate(frame).r,
                Color.Interpolate(frame).g,
                Color.Interpolate(frame).b,
                Color.Interpolate(frame).a);

            float width = Resolution.Interpolate(frame).x;
            float height = Resolution.Interpolate(frame).y;

            int tileSize = TileSize.Interpolate(frame).value;
            int crossSize = CrossSize.Interpolate(frame).value;

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
