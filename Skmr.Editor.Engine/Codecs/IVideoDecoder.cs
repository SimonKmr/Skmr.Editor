using Skmr.Editor.Engine.Data.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Engine.Codecs
{
    public interface IVideoDecoder : IDisposable
    {
        public bool TryDecode(byte[] frame, out Image<RGB>? result);
    }
}
