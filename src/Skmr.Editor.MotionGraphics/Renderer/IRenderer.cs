using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;

namespace Skmr.Editor.MotionGraphics.Renderer
{
    public interface IRenderer
    {
        public void Render();
        public Action<int, Frame<RGBA>> FrameRendered { get; set; }
    }
}
