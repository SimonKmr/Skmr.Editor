using Skmr.Editor.Engine.Y4M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Engine.Bitstreams.Y4M
{
    public class Reader : IDisposable
    {
        private int width = 480;
        private int height = 270;
        private Stream stream;

        public Reader(Stream stream)
        {
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

        public void Read(out Frame frame)
        {
            while (stream.ReadByte() != '\n') ;
            var buffer = new byte[width * height * 3 / 2];
            stream.Read(buffer, 0, buffer.Length);
            frame = new Frame(width, height, buffer);
        }

        public void Dispose()
        {
            stream.Dispose();
        }
    }
}
