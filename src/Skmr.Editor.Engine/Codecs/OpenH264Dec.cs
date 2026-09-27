using OpenH264Lib;
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;

namespace Skmr.Editor.Engine.Codecs
{
    public class OpenH264Dec : IVideoDecoder
    {
        //https://encodingwissen.de/codecs/x264/technik/


        public const string dllPath = "Dlls\\openh264-2.3.1-win64.dll";
        private readonly Decoder decoder;

        public int Width { get; set; }
        public int Height { get; set; }

        public OpenH264Dec(int width, int height)
        {
            Width = width;
            Height = height;

            decoder = new Decoder(dllPath);
        }

        public unsafe bool TryDecode(byte[] frame, out Frame<RGB>? result)
        {
            var size = Width * Height * 3;
            var data = decoder.Decode(frame, frame.Length);
            result = null;

            if (data == null) return false;

            byte[] arr = new byte[size];
            for (int p = 0; p < size; p++)
            {
                arr[p] = data[p];
            }

            result = RGBArrayToImage(arr, Width, Height);
            return true;
        }

        private Frame<RGB> RGBArrayToImage(byte[] arr, int width, int height)
        {
            if (width * height * 3 != arr.Length)
            {
                throw new ArgumentException("Buffer length does not match width/height for BGR data.");
            }

            var pixels = new RGB[width * height];

            for (int p = 0; p < arr.Length; p += 3)
            {
                pixels[p / 3] = new RGB(arr[p + 2], arr[p + 1], arr[p + 0]);
            }

            return new Frame<RGB>(width, height, pixels);
        }

        public void Dispose()
        {
            decoder.Dispose();
        }
    }
}
