using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics.Attributes;
using Skmr.Editor.MotionGraphics.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Elements
{
    public class Text : IElement
    {
        public string SourceText { get; set; } = String.Empty;
        public string FontFile { get; set; } = String.Empty;
        public float TextSize { get; set; } = 64.0f;
        public HorizontalAlignment TextAlignment { get; set; } = HorizontalAlignment.Center;
        public IAttribute<Vec2D> Position { get; set; }
        public IAttribute<RGBA> Color { get; set; }
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

                if(FontFile != String.Empty)
                {
                    paint.Typeface = SKTypeface.FromFile(FontFile);
                }
                
                paint.TextSize = TextSize;
                paint.IsAntialias = true;
                paint.IsStroke = false;
                paint.TextAlign = ToSKTextAlign(TextAlignment);

                var color = Color.GetFrame(frame);

                paint.Color = new SKColor(
                    color.r,
                    color.g,
                    color.b,
                    color.a);

                var pos = Position.GetFrame(frame);
                
                canvas.DrawText(
                    SourceText,
                    pos.x,
                    pos.y,
                    paint);
            }

        }
    }
}
