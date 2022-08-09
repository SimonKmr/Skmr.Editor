using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Analyzer.ComputerVision
{
    public class LeagueOfLegends
    {
        public static Position[] GetHealthbars(Bitmap image)
        {
            List<Position> positions = new List<Position>();
            using (Bitmap bitmap = image)
            {
                Image<Bgr, Byte> img = bitmap.ToImage<Bgr, Byte>();

                //Split off Red Segments
                var imgHSV = img.Convert<Hsv, Byte>();


                //GetPosition of Red Segments
                var imgRed = imgHSV.InRange(new Hsv(0, 170, 120), new Hsv(8, 255, 255));
                Utility.AddPositionsToList(positions,imgRed);

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
    }
}
