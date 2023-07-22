namespace Skmr.Editor.Engine.Y4M
{
    public interface IY4MContainer : IIndexable<Frame>
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int FrameRate { get; set; }
        public string ColorSpace { get; set; }

            

        public int Size
            => throw new NotImplementedException();
        public int FrameSize
            => throw new NotImplementedException();
        public int FrameBodySize
            => throw new NotImplementedException();
        public int FrameHeaderSize 
            => throw new NotImplementedException();
    }
}
