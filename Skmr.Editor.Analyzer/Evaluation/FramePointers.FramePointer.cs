namespace Skmr.Editor.Director
{
    public partial class FramePointers
    {
        public class FramePointer
        {
            public FramePointer(int start, int duration)
            {
                Start = start;
                Duration = duration;
            }
            public int Start { get; set; }
            public int Duration { get; set; }
        }
    }
}
