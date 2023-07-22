namespace Skmr.Editor.Engine.Y4M
{
    public class Frame
    {
        private byte[] data;
        public  IY4MContainer Parent { get; internal set; }

        public Frame(IY4MContainer parent)
        {
            this.Parent = parent;
            data = new byte[parent.FrameBodySize];
        }

        public Frame(IY4MContainer parent,byte[] data)
        {
            if (parent.FrameBodySize != data.Length) 
                throw new Exception();

            this.Parent = parent;
            this.data = data;
        }

        public byte[] Get()
        {
            var result = new byte[data.Length];
            Array.Copy(data, result, result.Length);
            return result;
        }

        public byte[] Get(Channel channel)
        {
            int ySize = Parent.Width * Parent.Height;
            int cbSize = ySize / 4;
            int crSize = ySize / 4;

            switch (channel)
            {
                case Channel.Y:
                    byte[] yComponent = new byte[ySize];
                    Array.Copy(data, 0, yComponent, 0, ySize);
                    return yComponent;
                case Channel.Cb:
                    byte[] cbComponent = new byte[cbSize];
                    Array.Copy(data, ySize, cbComponent, 0, cbSize);
                    return cbComponent;
                case Channel.Cr:
                    byte[] crComponent = new byte[crSize];
                    Array.Copy(data, ySize + cbSize, crComponent, 0, crSize);
                    return crComponent;
            }

            throw new Exception();
        }

        public byte Get(Channel channel, int x, int y)
            => data[GetIndex(channel, x, y)];

        public void Set(Channel channel, int x, int y, byte value)
            => data[GetIndex(channel, x, y)] = value;

        private int GetIndex(Channel channel, int x, int y)
        {
            int baseOffset = Parent.FrameHeaderSize;
            int ySize = Parent.Width * Parent.Height;
            int cbSize = ySize / 4;

            switch (channel)
            {
                case Channel.Y:  return baseOffset;
                case Channel.Cb: return baseOffset + ySize;
                case Channel.Cr: return baseOffset + ySize + cbSize;
            }
                
            throw new Exception();
        }

        public Frame Clone()
        {
            return new Frame(Parent)
            {
                data = (byte[])data.Clone(),
            };
        }
    }
}
