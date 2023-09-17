namespace Skmr.Editor.Engine.Y4M
{
    public class Frame
    {
        private byte[] data;

<<<<<<< HEAD
        public int Width { get; set; }
        public int Height { get; set; }
        public int Size 
            => SizeHeader + SizeBody;

        public int SizeHeader
            => "FRAME\n".Length;

        public int SizeBody
            => Width * Height * 3 / 2;
=======
        public int Width { get; }
        public int Height { get; }

        public int Size => (int)(Width * Height * 1.5f);
>>>>>>> ba52b126888bc894e876e48cab602e7ceaef74b3

        public Frame(int width, int height)
        {
            Width = width;
            Height = height;
<<<<<<< HEAD
            data = new byte[SizeBody];
=======

            data = new byte[Size];
>>>>>>> ba52b126888bc894e876e48cab602e7ceaef74b3
        }

        public Frame(int width, int height, byte[] data)
        {
            Width = width;
            Height = height;
<<<<<<< HEAD
            if (data.Length != SizeBody) throw new Exception();
=======

            if (data.Length != Size) throw new Exception();

>>>>>>> ba52b126888bc894e876e48cab602e7ceaef74b3
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
                case Channel.Y:  return baseOffset;
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
