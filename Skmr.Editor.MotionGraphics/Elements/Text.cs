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
            LetterAnimation(canvas, this, frame);

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

                

                //var pos = Position.GetFrame(frame);
                
                //canvas.DrawText(
                //    SourceText,
                //    pos.x,
                //    pos.y,
                //    paint);
            }

        }

        public static void LetterAnimation(SKCanvas canvas, Text ctx, int frame)
        {
            using (var paint = new SKPaint())
            {
                if (ctx.FontFile != String.Empty)
                {
                    paint.Typeface = SKTypeface.FromFile(ctx.FontFile);
                }

                paint.TextSize = ctx.TextSize;
                paint.IsAntialias = true;
                paint.IsStroke = false;
                paint.TextAlign = SKTextAlign.Left;
                
                var text = ctx.SourceText;
                var charPoints = paint.GetGlyphPositions(text, new SKPoint(0, 0));
                var charWidth = paint.GetGlyphWidths(text);
                var n = charPoints.Length;

                for (int i = 0; i < text.Length; i++)
                {
                    var pos = ctx.Position.GetFrame(frame);
                    var color = ctx.Color.GetFrame(frame - (i * 10) / text.Length);

                    paint.Color = new SKColor(
                        color.r,
                        color.g,
                        color.b,
                        color.a);

                    var xPos = charPoints[i].X + pos.x;
                    var yPos = charPoints[i].Y + pos.y;

                    switch (ctx.TextAlignment)
                    {
                        case HorizontalAlignment.Center:
                            xPos -= (charPoints[n - 1].X + charWidth[n - 1]) / 2;
                            break;
                        case HorizontalAlignment.Left:
                            xPos -= charPoints[n - 1].X + charWidth[n - 1];
                            break;
                        default:
                            xPos += 0;
                            break;
                    }

                    var character = text[i].ToString();
                    canvas.DrawText(character, xPos, yPos, paint);
                }
            }
        }
    }
}
