using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Engine.Codecs
{
    public class OpenH264Dec
    {
        //https://encodingwissen.de/codecs/x264/technik/


        private const string dllPath = "Dlls\\openh264-2.3.1-win64.dll";
        private readonly OpenH264Lib.Decoder decoder;

        public int Width { get; set; }
        public int Height { get; set; }

        public OpenH264Dec(int width, int height)
        {
            Width = width;
            Height = height;

            decoder = new OpenH264Lib.Decoder(dllPath);
        }

        public unsafe bool TryDecode(byte[] frame, out Y4M.Frame? result)
        {
            var size = Width * Height * 3 / 2;
            var bytes = decoder.Decode(frame, frame.Length);
            result = null;

            if (bytes == null) return false;
            var data = new byte[size];

            for (int i = 0; i < size; i++)
                data[i] = bytes[i];

            result = new Y4M.Frame(Width, Height, data);
            return true;
        }
    }
}
