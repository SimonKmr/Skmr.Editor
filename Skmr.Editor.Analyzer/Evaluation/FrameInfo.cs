using Skmr.Editor.Engine;
using Skmr.Editor.Analyzer.ComputerVision;
using Skmr.Editor.Data.Colors;


namespace Skmr.Editor.Director
{
    public class FrameInfo
    {
        Feature[] Features { get; set; }
        public FrameInfo(Feature[] features)
        {
            Features = features;
        }

        public FrameInfo(Image<RGB> image)
        {

        }
    }
}
