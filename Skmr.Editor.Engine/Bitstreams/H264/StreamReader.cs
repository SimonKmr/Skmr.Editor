using Skmr.Editor.Engine.Y4M;

namespace Skmr.Editor.Engine.Bitstreams.H264
{
    public class StreamReader : IDisposable
    {
        private Stream _stream;
        public StreamReader(Stream stream)
        {
            _stream = stream;
            _stream.Position = 4;
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        public void ReadFrame(out Frame frame)
        {
            List<byte> tmp = new List<byte>();
            var buffer = new byte[4];
            int num = -1;
            while (num != 16777216)
            {
                var b = (byte)_stream.ReadByte();
                buffer[(_stream.Position - 1) % 4] = b;
                num = BitConverter.ToInt32(buffer, 0);

                tmp.Add(b);
            }


            var raw = tmp.ToArray()[0..(tmp.Count - 4)];
            var result = new byte[tmp.Count + 4];

            result[0] = 0;
            result[1] = 0;
            result[2] = 0;
            result[3] = 1;

            Array.Copy(raw, 0, result, 4, raw.Length);

            frame = new Frame(480, 270, result);
        }
    }
}
