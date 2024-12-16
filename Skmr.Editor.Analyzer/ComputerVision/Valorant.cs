using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Drawing;

namespace Skmr.Editor.Analyzer.ComputerVision
{
    public class Valorant : IVision
    {
        public static bool IsValorant(Bitmap image)
        {
            return true;
        }

        public static Feature[] GetEnemyPositions(Bitmap image)
        {
            List<Feature> positions = new List<Feature>();
            using (Bitmap bitmap = image)
            {
                Image<Bgr, Byte> img = bitmap.ToImage<Bgr, Byte>();

                //Split off Red Segments
                var imgHSV = img.Convert<Hsv, Byte>();
                var imgRed = imgHSV.InRange(new Hsv(0, 170, 120), new Hsv(8, 255, 255));
                var imgMasked = RegionOfInterest(imgRed, GetValorantMask());

                //GetPosition of Red Segments
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                Mat hirarchy = new Mat();
                CvInvoke.FindContours(imgRed, contours, hirarchy, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

                for (int i = 0; i < contours.Size; i++)
                {
                    if (contours[i].Size > 4 && contours[i].Size < 40)
                    {
                        var p = Feature.Create("Healthbar", contours[i].ToArray());
                        if (p.Length > 10 && p.Height > 5)
                        {
                            positions.Add(p);
                        }
                    }
                }

                img.Dispose();
                imgHSV.Dispose();
                imgRed.Dispose();
                imgMasked.Dispose();
            }
            throw new NotImplementedException();

        }
        public static Image<Gray, Byte> RegionOfInterest(Image<Gray, Byte> image, VectorOfPoint points)
        {
            var mask = new Image<Gray, Byte>(image.Width, image.Height);
            CvInvoke.FillPoly(mask, points, new MCvScalar(255));
            CvInvoke.BitwiseAnd(image, mask, image);
            return image;
        }
        public static VectorOfPoint GetValorantMask()
        {
            throw new NotImplementedException();
        }

        public Feature[] Detect(Engine.Image image)
        {
            throw new NotImplementedException();
        }
    }
}
