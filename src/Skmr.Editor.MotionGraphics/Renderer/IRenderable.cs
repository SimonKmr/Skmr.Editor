using Skmr.Editor.Data;
using Skmr.Editor.Data.Interfaces;

namespace Skmr.Editor.MotionGraphics.Renderer
{
    public interface IRenderable<T> where T : IColorModel
    {
        Frame<T> GetFrame(int frame);
    }
}
