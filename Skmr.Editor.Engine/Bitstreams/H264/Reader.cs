using Skmr.Editor.Engine.Codecs.Apis.Rav1e;
using Skmr.Editor.Engine.Y4M;

namespace Skmr.Editor.Engine.Bitstreams.H264
{
    public class Reader : IDisposable
    {
        private Stream _stream;
        private byte[] _buffer = new byte[4];
        public int Width { get; }
        public int Height { get; }

        public Reader(Stream stream, int width, int height)
        {
            Width = width;
            Height = height;

            _stream = stream;
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        public bool Read(out byte[]? frame)
        {
            frame = null;
            List<byte> tmp = new List<byte>();
            int num = -1;

            while (num != 16777216)
            {
                var b = _stream.ReadByte();
                if (b == -1)
                    return false;

                var bbyte = (byte)b;

                _buffer[0] = _buffer[1];
                _buffer[1] = _buffer[2];
                _buffer[2] = _buffer[3];
                _buffer[3] = bbyte;
                num = BitConverter.ToInt32(_buffer, 0);
                
                tmp.Add(bbyte);
            }

            var raw = tmp.ToArray()[0..(tmp.Count - 4)];
            var result = new byte[tmp.Count + 4];

            result[0] = 0;
            result[1] = 0;
            result[2] = 0;
            result[3] = 1;

            Array.Copy(raw, 0, result, 4, raw.Length);
            frame = result;

            return true;
        }

        public bool ByStartCode(out byte[]? frame)
        {
            frame = null;
            List<byte> tmp = new List<byte>();
            int num = -1;

            while (num != 16777216)
            {
                var b = _stream.ReadByte();
                if (b == -1)
                    return false;

                var bbyte = (byte)b;

                _buffer[0] = _buffer[1];
                _buffer[1] = _buffer[2];
                _buffer[2] = _buffer[3];
                _buffer[3] = bbyte;
                num = BitConverter.ToInt32(_buffer, 0);

                tmp.Add(bbyte);
            }

            frame = tmp.ToArray()[0..(tmp.Count - 4)];

            return true;
        }

        public bool ByLength(out byte[]? frame)
        {
            frame = null;

            var b = _stream.ReadByte();
            if (b == -1)
                return false;

            _stream.Position--;

            var frameLengthBuffer = new byte[4];
            _stream.Read(frameLengthBuffer, 0, 4);
            var frameLength = BitConverter.ToInt32(frameLengthBuffer);

            frame = new byte[frameLength];
            _stream.Read(frame);

            return true;
        }
    }
}
