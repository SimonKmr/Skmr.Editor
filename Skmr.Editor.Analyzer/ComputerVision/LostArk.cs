using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Skmr.Editor;
using System.Drawing;

namespace Skmr.Editor.Analyzer.ComputerVision
{
    public class LostArk : IVision
    {
        public static Feature[] GetPositions(Engine.Image<Data.Colors.RGB> image)
        {
            Feature[] healthbars = GetHealthbarPositions(image);
            throw new NotImplementedException();
        }
        public static Feature[] GetHealthbarPositions(Engine.Image<Data.Colors.RGB> image)
        {
            List<Feature> positions = new List<Feature>();
            var bgrMap = image.ToBgrMap();
            Image<Bgr, Byte> img = new Image<Bgr, Byte>(bgrMap);

            //Split off Red Segments
            var imgHSV = img.Convert<Hsv, Byte>();
            var imgRed = imgHSV.InRange(new Hsv(0, 170, 120), new Hsv(8, 255, 255));
            var imgMasked = Utility.RegionOfInterest(imgRed, GetLostArkMask());

            Utility.AddPositionsToList(positions, imgMasked);

            //Dispose all the images, because else the ram clutters up
            img.Dispose();
            imgHSV.Dispose();
            imgRed.Dispose();
            imgMasked.Dispose();
            
            return positions.ToArray();
        }
        public static VectorOfPoint GetLostArkMask()
        {
            return new VectorOfPoint(new Point[]
            {
                new Point(300,150),
                new Point(1920,150),
                new Point(1920,900),
                new Point(300,900)
            });
        }
        public static VectorOfPoint GetLostArkMaskSmallUI()
        {
            return new VectorOfPoint(new Point[]
            {
                new Point(300,75),
                new Point(1660,75),
                new Point(1660,980),
                new Point(300,980)
            });
        }

        public Feature[] Detect(Engine.Image<Data.Colors.RGB> image)
        {
            return GetHealthbarPositions(image);
        }
    }
}









