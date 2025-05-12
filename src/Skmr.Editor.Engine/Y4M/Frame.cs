namespace Skmr.Editor.Engine.Y4M
{
    public class Frame
    {
        private byte[] data;

        public int Width { get; set; }
        public int Height { get; set; }

        public int Size
            => Width * Height * 3 / 2;

        public Frame(int width, int height)
        {
            Width = width;
            Height = height;
            data = new byte[Size];
        }

        public Frame(int width, int height, byte[] data)
        {
            Width = width;
            Height = height;

            if (data.Length != Size)
                throw new Exception();

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
            int ySize = Width * Height;
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
            int baseOffset = Size;
            int ySize = Width * Height;
            int cbSize = ySize / 4;

            switch (channel)
            {
                case Channel.Y: return baseOffset;
                case Channel.Cb: return baseOffset + ySize;
                case Channel.Cr: return baseOffset + ySize + cbSize;
            }

            throw new Exception();
        }
        public byte[] GetData()
        {
            var res = new byte[data.Length];
            Array.Copy(data, res, data.Length);
            return res;
        }
    }
}
