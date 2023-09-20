using Skmr.Editor.Engine.Y4M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Engine.Bitstreams.Y4M
{
    public class StreamWriter : IDisposable
    {
        FileStream _fileStream;
        public StreamWriter(string path, int width, int height, int fps = 30)
        {
            _fileStream = File.Open(path, FileMode.OpenOrCreate);

            var header = $"YUV4MPEG2 W{width} H{height} F{fps}:1 Ip A1:1 C420mpeg2\n";
            var chars = header.ToCharArray();
            var bytes = new byte[chars.Length];

            for (int i = 0; i < chars.Length; i++)
            {
                bytes[i] = (byte)chars[i];
            }

            _fileStream.Write(bytes, 0, chars.Length);
        }

        public void Dispose()
        {
            _fileStream.Dispose();
        }

        public void Write(Frame frame)
        {
            byte[] frameHead = new byte[] { 0x46, 0x52, 0x41, 0x4D, 0x45, 0x0A };
            _fileStream.Write(frameHead);
            _fileStream.Write(frame.GetData());
        }
    }
}
