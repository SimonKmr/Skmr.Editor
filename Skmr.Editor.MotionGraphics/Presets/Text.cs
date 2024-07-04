using SkiaSharp;
using Skmr.Editor.MotionGraphics.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Presets
{
    public class Text
    {
        public static void LetterAnimation(SKCanvas canvas, Elements.Text ctx, int frame)
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
                    var pos = ctx.Position.GetFrame(frame - (i * 10) / text.Length);
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

        public static void ScrollingText(SKCanvas canvas, Elements.Text ctx, int frame)
        {

        }
    }
}
