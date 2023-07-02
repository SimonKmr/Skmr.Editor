using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Engine
{
    public class Y4M
    {
        public IndexOutOfRangeException frameOutOfRange = new IndexOutOfRangeException("Length of bytes does not match the size of the frames");
        public string Path { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string ColorSpace { get; private set; }
        public int FrameSize { get => Width * Height * 3 / 2; }
        public int FileHeaderSize { get; private set; }
        public int FrameHeaderSize { get; private set; }
        public int Length 
        { 
            get 
            {
                int result = 0;
                using (Stream source = File.OpenWrite(Path))
                {
                    var bytes = source.Length - FrameHeaderSize;
                    var frames = bytes / (FrameSize + FrameHeaderSize);
                    result = Convert.ToInt32(frames);
                }
                return result;
            } 
        }

        public Y4M(string path)
        {
            Path = path;
            //Read Header
            using (var sr = new StreamReader(path))
            {
                StringBuilder sb = new StringBuilder();
                var fileHeader = sr.ReadLine();
                var frameHeader = sr.ReadLine();

                FileHeaderSize = fileHeader.Length + 1;
                if (frameHeader is not null) FrameHeaderSize = frameHeader.Length + 1;
                else FrameHeaderSize = 6;
            }
            //Parse Header
            Width = 1920;
            Height = 1080;
            ColorSpace = "4:2:0";
        }

        /// <summary>
        /// gets the byte sequence for a specific frame
        /// </summary>
        public byte[] Get(int frame)
        {
            byte[] bytes = new byte[FrameSize];
            using (Stream source = File.OpenRead(Path))
            {
                //Position Header
                source.Position = FileHeaderSize + FrameHeaderSize;
                source.Position += (long)(FrameSize + FrameHeaderSize) * frame;
                source.Read(bytes, 0, bytes.Length);
            }
            return bytes;
        }

        /// <summary>
        /// sets the byte sequence for a specific frame
        /// </summary>
        public void Set(int frame, byte[] bytes)
        {
            if (bytes.Length != FrameSize) throw frameOutOfRange;


            using (Stream source = File.OpenWrite(Path))
            {
                //Position Header
                source.Position = FileHeaderSize + FrameHeaderSize;
                source.Position += (long)(FrameSize + FrameHeaderSize) * (long)frame;
                source.Write(bytes, 0, bytes.Length);
            }
        }

        /// <summary>
        /// creates a new element at the end of the file and returns the index of the element
        /// </summary>
        public int Append()
        {
            byte[] bytes = new byte[FrameSize];
            string frameHeaderBase = "FRAME\n"; 
            byte[] headerBytes = frameHeaderBase.Select(s => Convert.ToByte(s)).ToArray();

            using (Stream source = File.Open(Path,FileMode.Append))
            {
                source.Write(headerBytes, 0, headerBytes.Length);
                source.Write(bytes, 0, bytes.Length);
            }

            return Length - 1;
        }

        /// <summary>
        /// removes the last element
        /// </summary>
        public void Drop()
        {
            using (Stream source = File.OpenWrite(Path))
            {
                source.SetLength(source.Length - FrameSize - FrameHeaderSize);
            }
        }

        /// <summary>
        /// Creates a new y4m file
        /// </summary>
        public static Y4M Create(string path, int width, int height)
        {
            string header = $"YUV4MPEG2 W{width} H{height} F30:1 Ip A1:1 C420mpeg2 XYSCSS=420MPEG2 XCOLORRANGE=LIMITED\n";
            byte[] headerBytes = header.Select(s => Convert.ToByte(s)).ToArray();
            using (Stream source = File.Open(path,FileMode.Create))
            {
                source.Write(headerBytes, 0, headerBytes.Length);
            }

            return new Y4M(path);
        }
    }
}
