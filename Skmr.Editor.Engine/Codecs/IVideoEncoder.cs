using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Engine.Codecs
{
    internal interface IVideoEncoder : IDisposable
    {
        public EncoderState SendFrame(Image<RGB> image);
        public EncoderState ReceiveFrame(out Image<RGB> image);
        public void Flush();
    }
}
