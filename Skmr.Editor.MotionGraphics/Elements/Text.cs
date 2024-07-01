using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Elements
{
    public class Text : IElement
    {
        public string SourceText { get; set; } = String.Empty;
        public string FontFile { get; set; } = String.Empty;
        public float TextSize { get; set; } = 64.0f;
        public Attribute<Pos2D> Position { get; }
        public Attribute<RGBA> Color { get; }


        public Text()
        {
            Color = new Attribute<RGBA>();
            Position = new Attribute<Pos2D>();
        }

        public void DrawOn(int frame, SKCanvas canvas)
        {
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
                paint.TextAlign = SKTextAlign.Center;

                //set initial index as highest
                int cIndex = Color.Keyframes.Count - 1;
                int pIndex = Position.Keyframes.Count - 1;          

                var pos = Position.Interpolate(frame);
                var color = Color.Interpolate(frame);

                paint.Color = new SKColor(
                    color.r, 
                    color.g, 
                    color.b, 
                    color.a);
                
                canvas.DrawText(
                    SourceText,
                    pos.x,
                    pos.y,
                    paint);
            }

        }


    }
}
