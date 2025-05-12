using SkiaSharp;
namespace Skmr.Editor.MotionGraphics.Elements
{
    public interface IElement
    {
        public void DrawOn(int frame, SKCanvas canvas);
    }
}
