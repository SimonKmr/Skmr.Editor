using Skmr.Editor.MotionGraphics.Elements;

namespace Skmr.Editor.MotionGraphics.Sequences
{
    public interface ISequence
    {
        public List<IElement> Elements { get; }
        public Action<int, byte[]> FrameRendered { get; set; }
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }

        public void Render();
    }
}
