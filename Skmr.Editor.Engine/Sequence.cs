using Skmr.Editor.Data.Interfaces;
using Skmr.Editor.Engine.Codecs;
using Skmr.Editor.Engine.Containers;

namespace Skmr.Editor.Engine
{
    public class Sequence : ISequence
    {
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }

        IVideoContainer video;
        IVideoDecoder decoder;

        public void Render()
        {
            throw new NotImplementedException();
        }
    }
}
