using Skmr.Editor.MotionGraphics.Elements;

namespace Skmr.Editor.MotionGraphics.Sequences
{
    public interface ISequence
    {
        public List<IElement> Elements { get; }
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }
    }
}
