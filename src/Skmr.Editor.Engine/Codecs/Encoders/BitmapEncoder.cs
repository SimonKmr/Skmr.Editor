using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;

namespace Skmr.Editor.Engine.Codecs.Encoders
{
    public class BitmapEncoder : IVideoEncoder
    {
        byte[]? currentFrame;
        bool isFlushed = false;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Flush() { isFlushed = true; }

        public EncoderState TryEncode(Frame<RGB> image, out byte[]? result)
        {
            throw new NotImplementedException();
        }
    }
}
