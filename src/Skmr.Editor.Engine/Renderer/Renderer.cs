using Skmr.Editor.Data.Colors;
using Skmr.Editor.Data.Interfaces;
using Skmr.Editor.Engine.Codecs;

namespace Skmr.Editor.MotionGraphics.Renderer
{
    public class Renderer(IRenderable<RGB> sequence, IVideoEncoder encoding)
    {
        public IRenderable<RGB> Sequence { get; private set; } = sequence;
        public IVideoEncoder Encoding { get; private set; } = encoding;
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
            var currentFrame = Sequence.GetFrame(frame);
            List<byte> bytes = new List<byte>();
            foreach (var val in currentFrame.GetSpan().ToArray().Select(x => new byte[] { x.r, x.g, x.b }))
            {
                bytes.AddRange(val);
            }

            return bytes.ToArray();
        }
    }
}
