using OpenH264Lib;
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

        public unsafe bool TryDecode(byte[] frame, out Image<RGB>? result)
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

        private Image<RGB> RGBArrayToImage(byte[] arr, int width, int height)
        {
            var result = new Image<RGB>(width, height);
            for (int p = 0; p < arr.Length; p += 3)
            {
                var rgb = new RGB(arr[p + 2], arr[p + 1], arr[p + 0]);

                var x = (p / 3) % width;
                var y = (p / 3) / width;

                result.Set(x, y, rgb);
            }
            return result;
        }

        public void Dispose()
        {
            decoder.Dispose();
        }
    }
}
