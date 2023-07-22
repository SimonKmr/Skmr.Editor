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

        public int Size
            => (FrameBodySize + FrameHeaderSize) * Frames.Length;
        public int FrameSize
            => FrameBodySize + FrameHeaderSize;
        public int FrameBodySize
            => Width * Height * 3 / 2;
        public int FrameHeaderSize
            => "FRAME\n".Length;

        /// <summary>
        /// Creates a frame for the sequence
        /// </summary>
        /// <returns></returns>
        public Frame ProvideFrame()
        {
            return new Frame(this);
        }
    } 
}
