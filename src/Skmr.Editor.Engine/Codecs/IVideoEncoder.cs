using Skmr.Editor.Data.Colors;

namespace Skmr.Editor.Engine.Codecs
{
    public interface IVideoEncoder : IDisposable
    {
        public EncoderState SendFrame(Image<RGB> image);
        public EncoderState ReceiveFrame(out byte[]? image);
        public void Flush();
    }
}
