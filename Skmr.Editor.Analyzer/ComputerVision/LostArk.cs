using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Drawing;

namespace Skmr.Editor.Analyzer.ComputerVision
{
    public class LostArk
    {
        public static Position[] GetPositions(Bitmap image)
        {
            Position[] healthbars = GetHealthbarPositions(image);
            throw new NotImplementedException();
        }
        public static Position[] GetHealthbarPositions(Bitmap image)
        {
            List<Position> positions = new List<Position>();
            using (Bitmap bitmap = image)
            {
                Image<Bgr, Byte> img = bitmap.ToImage<Bgr, Byte>();

                //Split off Red Segments
                var imgHSV = img.Convert<Hsv, Byte>();
                var imgRed = imgHSV.InRange(new Hsv(0, 170, 120), new Hsv(8, 255, 255));
                var imgMasked = RegionOfInterest(imgRed, GetLostArkMask());

                //GetPosition of Red Segments
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                Mat hirarchy = new Mat();
                CvInvoke.FindContours(imgRed, contours, hirarchy, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

                //Filter Contours
                for (int i = 0; i < contours.Size; i++)
                {
                    if (contours[i].Size > 4 && contours[i].Size < 40)
                    {
                        var p = Position.Create("Healthbar", contours[i].ToArray());
                        if (p.Length > 10 && p.Height <= 5)
                        {
                            positions.Add(p);
                        }
                    }
                }

                //Dispose all the images, because else the ram clutters up
                img.Dispose();
                imgHSV.Dispose();
                imgRed.Dispose();
                imgMasked.Dispose();
            }
            return positions.ToArray();
        }
        public static Image<Gray, Byte> RegionOfInterest(Image<Gray, Byte> image, VectorOfPoint points)
        {
            var mask = new Image<Gray, Byte>(image.Width, image.Height);
            CvInvoke.FillPoly(mask, points, new MCvScalar(255));
            CvInvoke.BitwiseAnd(image, mask, image);
            return image;
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
    }
}









