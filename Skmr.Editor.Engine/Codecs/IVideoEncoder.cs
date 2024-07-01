using Skmr.Editor.Engine.Data.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Engine.Codecs
{
    public interface IVideoEncoder : IDisposable
    {
        public EncoderState SendFrame(Image<RGB> image);
        public EncoderState ReceiveFrame(out byte[]? image);
        public void Flush();
    }
}
