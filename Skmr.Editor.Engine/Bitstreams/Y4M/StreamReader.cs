using Skmr.Editor.Engine.Y4M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Engine.Bitstreams.Y4M
{
    public class StreamReader : IDisposable
    {
        private int width = 480;
        private int height = 270;
        private FileStream _fileStream;

        public StreamReader(string path)
        {
            _fileStream = File.Open(path, FileMode.Open);

            while (true)
            {
                var value = _fileStream.ReadByte();
                var breakChar = (int)'\n';
                if (value == breakChar) break;
            }

            var end = (int)_fileStream.Position + 1;
            _fileStream.Position = 0;

            var buffer = new byte[end];
            _fileStream.Read(buffer, 0, end);
        }

        public void Read(out Frame frame)
        {
            while (_fileStream.ReadByte() != '\n') ;
            var buffer = new byte[width * height * 3 / 2];
            _fileStream.Read(buffer, 0, buffer.Length);
            frame = new Frame(width, height, buffer);
        }

        public void Dispose()
        {
            _fileStream.Dispose();
        }
    }
}
