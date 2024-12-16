using Skmr.Editor.Data.Colors;
using Skmr.Editor.Engine.Y4M;

namespace Skmr.Editor.Engine.Bitstreams.Y4M
{
    public class Reader : IDisposable
    {
        public int Width { get; }
        private int Height { get; }
        private Stream stream;

        public Reader(Stream stream, int width, int height)
        {
            Width = width;
            Height = height;

            this.stream = stream;

            while (true)
            {
                var value = this.stream.ReadByte();
                var breakChar = (int)'\n';
                if (value == breakChar) break;
            }

            var end = (int)this.stream.Position + 1;
            this.stream.Position = 0;

            var buffer = new byte[end];
            this.stream.Read(buffer, 0, end);
        }

        public void Read(out Image<RGB> frame)
        {
            while (stream.ReadByte() != '\n') ;
            var buffer = new byte[Width * Height * 3 / 2];
            stream.Read(buffer, 0, buffer.Length);
            frame = new Frame(Width, Height, buffer).ToImage();
        }

        public void Dispose()
        {
            stream.Dispose();
        }
    }
}
