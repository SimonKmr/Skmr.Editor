using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Enums;

namespace Skmr.Editor.MotionGraphics.Sequences
{
    public interface ISequence : IList<IElement>
    {
        public Action<int, byte[]> FrameRendered { get; set; }
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }

        public void Render(Encoding encoding);
    }
}
