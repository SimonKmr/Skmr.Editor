using Newtonsoft.Json;
using SkiaSharp;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics.Attributes;

namespace Skmr.Editor.MotionGraphics.Elements
{
    internal class Paint : ISubelement<SKPaint>
    {
        SKPaint paint;
        [JsonProperty] public IAttribute<RGBA> Color { get; set; }
        
        public Paint()
        {
            paint = new SKPaint();
        }

        public SKPaint GetValue(int frame)
        {
            paint.BlendMode = SKBlendMode.Color;
            return paint;
        }

        public void Dispose()
        {
            paint.Dispose();
        }
    }
}
