using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;

namespace Skmr.Editor.Analyzer.ComputerVision
{
    public interface IVision
    {
        public Feature[] Detect(Frame<RGB> image);
    }
}
