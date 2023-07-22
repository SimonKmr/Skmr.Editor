namespace Skmr.Editor.Engine.Y4M
{
    public class Sequence : IY4MContainer
    {
        public Frame this[int index] 
        { 
            get => Frames[index]; set 
            {
                var frame = value.Clone();
                frame.Parent = this;
                Frames[index] = frame;
            } 
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public int FrameRate { get; set; }
        public string ColorSpace { get; set; }
        private Frame[] Frames { get; set; }
    } 
}
