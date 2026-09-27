using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;

namespace Skmr.Editor.Engine.Renderer
{
    public interface IRenderer
    {
        public void Render();
        public Action<int, Frame<RGBA>> FrameRendered { get; set; }
    }
}
