using Skmr.Editor.Data.Colors;

namespace Skmr.Editor.Engine.Codecs
{
    public interface IVideoDecoder : IDisposable
    {
        public bool TryDecode(byte[] frame, out Image<RGB>? result);
    }
}
