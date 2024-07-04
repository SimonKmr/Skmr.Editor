using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics;
using Skmr.Editor.MotionGraphics.Attributes;
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
        public IAttribute<RGBA> Color { get; set; }
        public IAttribute<Vec2D> Resolution { get; set; }
        public IAttribute<Vec2D> Offset { get; set; }
        public IAttribute<Vec2D> Position { get; set; }
        public IAttribute<AInt> TileSize { get; set; }
        public IAttribute<AInt> StrokeWidth { get; set; }

        public Grid()
        {
            Color = new InterpolatedAttribute<RGBA>();
            Resolution = new InterpolatedAttribute<Vec2D>();
            Offset = new InterpolatedAttribute<Vec2D>();
            Position = new InterpolatedAttribute<Vec2D>();
            TileSize = new InterpolatedAttribute<AInt>();
            StrokeWidth = new InterpolatedAttribute<AInt>();
        }

        public void DrawOn(int frame, SKCanvas canvas)
        {
            var paint = new SKPaint();
            paint.Color = new SKColor(Color.GetFrame(frame).r, Color.GetFrame(frame).g, Color.GetFrame(frame).b, Color.GetFrame(frame).a);
            paint.StrokeWidth = StrokeWidth.GetFrame(frame).value;

            var resX = Resolution.GetFrame(frame).x;
            var tileSize = TileSize.GetFrame(frame).value;
            var offsetX = Offset.GetFrame(frame).x;
            
            var x = Position.GetFrame(frame).x;
            var y = Position.GetFrame(frame).y;

            float xOffset = resX % tileSize / 2 + offsetX;
            float yOffset = Resolution.GetFrame(frame).y % TileSize.GetFrame(frame).value / 2 + Offset.GetFrame(frame).y;

            float width = Resolution.GetFrame(frame).x;
            float height = Resolution.GetFrame(frame).y;

            //vertical
            for (int i = (int)xOffset; i < width; i += TileSize.GetFrame(frame).value)
                canvas.DrawLine(
                    new SKPoint(x + i,y),
                    new SKPoint(x + i,y + height),
                    paint);

            for (int i = (int)yOffset; i < height; i += TileSize.GetFrame(frame).value)
                canvas.DrawLine(
                    new SKPoint(x, y + i),
                    new SKPoint(x + width, y + i),
                    paint);
        }
    }
}
