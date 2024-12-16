using Skmr.Editor.Analyzer.ComputerVision;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.Engine;

namespace Skmr.Editor.Analyzer.Evaluation
{
    public class Director
    {
        private IScoreing scoring;
        private IVision vision;
        public Director(IVision vision, IScoreing scoreing)
        {
            this.vision = vision;
        }

        public float Evaluate(Image<RGB> image)
        {
            var features = GetFeatures(image);
            throw new NotImplementedException();
        }

        public Feature[] GetFeatures(Image<RGB> image)
        {
            List<Feature> features = new List<Feature>();
            foreach (var f in vision.Detect(image))
            {
                features.Add(f);
            }

            return features.ToArray();
        }
    }
}
