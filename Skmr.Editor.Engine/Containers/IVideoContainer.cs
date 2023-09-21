using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Engine.Containers
{
    public interface IVideoContainer
    {
        public Stream GetVideoStream();
    }
}
