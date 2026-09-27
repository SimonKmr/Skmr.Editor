using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;

namespace Skmr.Editor.Engine.Codecs
{
    public interface IVideoEncoder : IDisposable
    {
        EncoderState TryEncode(Frame<RGB> image, out byte[]? result);

        void Flush();
    }
}
