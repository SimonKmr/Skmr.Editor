using Skmr.Editor.Engine.Y4M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Engine.Bitstreams.Y4M
{
    public class Writer : IDisposable
    {
        private Stream stream;

        public Writer(Stream stream, int width, int height, int fps = 30)
        {
            this.stream = stream;

            var header = $"YUV4MPEG2 W{width} H{height} F{fps}:1 Ip A1:1 C420mpeg2\n";
            var chars = header.ToCharArray();
            var bytes = new byte[chars.Length];

            for (int i = 0; i < chars.Length; i++)
            {
                bytes[i] = (byte)chars[i];
            }

            this.stream.Write(bytes, 0, chars.Length);
        }

        public void Dispose()
        {
            stream.Dispose();
        }

        public void Write(Image<RGB> frame)
        {
            byte[] frameHead = new byte[] { 0x46, 0x52, 0x41, 0x4D, 0x45, 0x0A };
            stream.Write(frameHead);
            stream.Write(frame.ToFrame().GetData());
        }
    }
}
