using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Drawing;
using skmr = Skmr.Editor.Engine;

namespace Skmr.Editor.Analyzer.ComputerVision
{
    public class LeagueOfLegends : IVision
    {
        public static Feature[] GetHealthbars(Bitmap image)
        {
            List<Feature> positions = new List<Feature>();
            using (Bitmap bitmap = image)
            {
                Image<Bgr, Byte> img = bitmap.ToImage<Bgr, Byte>();

                //Split off Red Segments
                var imgHSV = img.Convert<Hsv, Byte>();


                //GetPosition of Red Segments
                var imgRed = imgHSV.InRange(new Hsv(0, 170, 120), new Hsv(8, 255, 255));
                Utility.AddPositionsToList(positions, imgRed);

                var imgBlue = imgHSV.InRange(new Hsv(100, 160, 200), new Hsv(110, 230, 255));
                Utility.AddPositionsToList(positions, imgBlue);

                var imgYellow = imgHSV.InRange(new Hsv(0, 100, 210), new Hsv(40, 255, 255));
                Utility.AddPositionsToList(positions, imgYellow);

                imgRed.Save("Red.jpg");
                imgYellow.Save("Yellow.jpg");

                img.Dispose();
                imgHSV.Dispose();
                imgRed.Dispose();
                imgBlue.Dispose();
                imgYellow.Dispose();
            }
            return positions.ToArray();

        }



        public static bool HasGrayScreen(Bitmap image)
        {
            using (Bitmap bitmap = image)
            {
                Image<Bgr, Byte> img = bitmap.ToImage<Bgr, Byte>();
            }
            return false;
        }

        public static VectorOfPoint GetMask()
        {
            throw new NotImplementedException();
        }

        public Feature[] Detect(Engine.Image<Data.Colors.RGB> image)
        {
            throw new NotImplementedException();
        }
    }
}
