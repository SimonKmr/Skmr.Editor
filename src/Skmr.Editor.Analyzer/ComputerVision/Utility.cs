﻿using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Skmr.Editor.Analyzer.ComputerVision
{
    public static class Utility
    {
        public static Image<Gray, Byte> RegionOfInterest(Image<Gray, Byte> image, VectorOfPoint points)
        {
            var mask = new Image<Gray, Byte>(image.Width, image.Height);
            CvInvoke.FillPoly(mask, points, new MCvScalar(255));
            CvInvoke.BitwiseAnd(image, mask, image);
            return image;
        }

        public static void AddPositionsToList(List<Feature> list, Image<Gray, Byte> imgColor)
        {
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hirarchy = new Mat();
            CvInvoke.FindContours(imgColor, contours, hirarchy, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            for (int i = 0; i < contours.Size; i++)
            {
                if (contours[i].Size > 4 && contours[i].Size < 40)
                {
                    var p = Feature.Create("Healthbar", contours[i].ToArray());
                    if (p.Length > 10 && p.Height > 5)
                    {
                        list.Add(p);
                    }
                }
            }
        }

        public static byte[,,] ToBgrMap(this Engine.Image<Data.Colors.RGB> image1)
        {

            throw new NotImplementedException();
        }
    }
}
