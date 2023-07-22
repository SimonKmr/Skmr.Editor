using System.Text;

namespace Skmr.Editor.Engine.Y4M
{
    public class File : IY4MContainer
    {
        public IndexOutOfRangeException frameOutOfRange = new IndexOutOfRangeException("Length of bytes does not match the size of the frames");
            
        public string Path { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FrameRate { get; set; }
        public string ColorSpace { get; set; }
            
        public int Size
            => (FrameBodySize + FrameHeaderSize) * Length + FileHeaderSize;
        public int FrameSize
            => FrameBodySize + FrameHeaderSize;
        public int FrameBodySize
            => Width * Height * 3 / 2;
        public int FrameHeaderSize
            => "FRAME\n".Length;
        public int FileHeaderSize{ get; private set; }

        public int Length
        {
            get
            {
                int result = 0;
                using (Stream source = System.IO.File.OpenWrite(Path))
                {
                    var bytes = source.Length - FrameHeaderSize;
                    var frames = bytes / FrameHeaderSize;
                    result = Convert.ToInt32(frames);
                }
                return result;
            }
        }

        public Frame this[int index] {
            get
            {
                byte[] bytes = new byte[FrameBodySize];
                using (Stream source = System.IO.File.OpenRead(Path))
                {
                    //Position Header
                    source.Position = FileHeaderSize + FrameHeaderSize;
                    source.Position += (long)(FrameBodySize + FrameHeaderSize) * index;
                    source.Read(bytes, 0, bytes.Length);
                }
                return new Frame(this,bytes);
            }
            set
            {
                if (value.Parent.FrameBodySize != FrameBodySize) 
                    throw new Exception();

                var frame = value.Clone();
                frame.Parent = this;
                var bytes = frame.Get();
                using (Stream source = System.IO.File.OpenWrite(Path))
                {
                    //Position Header
                    source.Position = FileHeaderSize + FrameHeaderSize;
                    source.Position += (long)FrameSize * index;
                    source.Write(bytes, 0, bytes.Length);
                }
            } 
        }

        public File(string path)
        {
            Path = path;
            //Read Header
            using (var sr = new StreamReader(path))
            {
                StringBuilder sb = new StringBuilder();
                var fileHeader = sr.ReadLine();
                var frameHeader = sr.ReadLine();

                FileHeaderSize = fileHeader.Length + 1;
            }
            //Parse Header
            Width = 1920;
            Height = 1080;
            ColorSpace = "4:2:0";
        }

        /// <summary>
        /// creates a new element at the end of the file and returns the index of the element
        /// </summary>
        public int Append()
        {
            string frameHeaderBase = "FRAME\n";
            byte[] bytes = new byte[frameHeaderBase.Length];
            byte[] headerBytes = frameHeaderBase.Select(s => Convert.ToByte(s)).ToArray();

            using (Stream source = System.IO.File.Open(Path, FileMode.Append))
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
            using (Stream source = System.IO.File.OpenWrite(Path))
            {
                source.SetLength(source.Length - FrameBodySize - FrameHeaderSize);
            }
        }

        /// <summary>
        /// Creates a frame for the sequence
        /// </summary>
        /// <returns></returns>
        public Frame ProvideFrame()
        {
            return new Frame(this);
        }

        /// <summary>
        /// Creates a new y4m file
        /// </summary>
        public static File Create(string path, int width, int height, int fps = 30)
        {
            string header = $"YUV4MPEG2 W{width} H{height} F{fps}:1 Ip A1:1 C420mpeg2 XYSCSS=420MPEG2 XCOLORRANGE=LIMITED\n";
            byte[] headerBytes = header.Select(s => Convert.ToByte(s)).ToArray();
            using (Stream source = System.IO.File.Open(path, FileMode.Create))
            {
                source.Write(headerBytes, 0, headerBytes.Length);
            }

            return new File(path);
        }

    }
    
}
