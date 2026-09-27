using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Enums;

namespace Skmr.Editor.MotionGraphics.Sequences
{
    public interface ISequence : IList<IElement>
    {
        public List<IElement> Elements { get; }
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }
    }
}
