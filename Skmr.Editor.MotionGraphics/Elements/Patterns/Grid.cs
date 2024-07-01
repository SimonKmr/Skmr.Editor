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
    public class Grid : IElement
    {
        public Attribute<RGBA> Color { get; set; } = new Attribute<RGBA>();
        public Attribute<AVec2D> Resolution { get; set; } = new Attribute<AVec2D>();
        public Attribute<AVec2D> Offset { get; set; } = new Attribute<AVec2D>();
        public Attribute<AVec2D> Position { get; set; } = new Attribute<AVec2D>();
        public Attribute<AInt> TileSize { get; set; } = new Attribute<AInt>();
        public Attribute<AInt> StrokeWidth { get; set; } = new Attribute<AInt>();

        public void DrawOn(int frame, SKCanvas canvas)
        {
            var paint = new SKPaint();
            paint.Color = new SKColor(Color.Interpolate(frame).r, Color.Interpolate(frame).g, Color.Interpolate(frame).b, Color.Interpolate(frame).a);
            paint.StrokeWidth = StrokeWidth.Interpolate(frame).value;

            var resX = Resolution.Interpolate(frame).x;
            var tileSize = TileSize.Interpolate(frame).value;
            var offsetX = Offset.Interpolate(frame).x;
            
            var x = Position.Interpolate(frame).x;
            var y = Position.Interpolate(frame).y;

            float xOffset = resX % tileSize / 2 + offsetX;
            float yOffset = Resolution.Interpolate(frame).y % TileSize.Interpolate(frame).value / 2 + Offset.Interpolate(frame).y;

            float width = Resolution.Interpolate(frame).x;
            float height = Resolution.Interpolate(frame).y;

            //vertical
            for (int i = (int)xOffset; i < width; i += TileSize.Interpolate(frame).value)
                canvas.DrawLine(
                    new SKPoint(x + i,y),
                    new SKPoint(x + i,y + height),
                    paint);

            for (int i = (int)yOffset; i < height; i += TileSize.Interpolate(frame).value)
                canvas.DrawLine(
                    new SKPoint(x, y + i),
                    new SKPoint(x + width, y + i),
                    paint);
        }
    }
}
