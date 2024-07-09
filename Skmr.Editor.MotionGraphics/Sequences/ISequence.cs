namespace Skmr.Editor.MotionGraphics.Sequences
{
    public interface ISequence
    {
        public Action<int, byte[]> FrameRendered { get; set; }
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }

        public void Render();
    }
}
