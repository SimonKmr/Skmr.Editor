using Skmr.Editor.Data.Colors;
using Skmr.Editor.Data.Interfaces;

namespace Skmr.Editor.Engine.Renderer
{
    public class RawRenderer(IRenderable<RGBA> sequence)
    {
        public IRenderable<RGBA> Sequence { get; private set; } = sequence;
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }
        public int MaxThreads { get; private set; } = 4;
        public Action<int, byte[]> FrameRendered { get; set; } = delegate { };

        public void Render()
        {
            Parallel.For(StartFrame, EndFrame, new ParallelOptions() { MaxDegreeOfParallelism = MaxThreads }, (i, state) =>
            {
                RenderFrame(i);
            });
        }

        public byte[] RenderFrame(int frame)
        {
            throw new NotImplementedException();
        }
    }
}
