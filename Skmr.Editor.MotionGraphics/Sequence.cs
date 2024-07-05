using SkiaSharp;
using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Enums;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Skmr.Editor.MotionGraphics
{
    public class Sequence
    {
        private SKImageInfo info;

        public Encoding Encoding { get; set; }

        public Sequence(int width, int height)
        {
            Resolution = (width, height);
            info = new SKImageInfo(Resolution.width, Resolution.height);
        }

        public (int width, int height) Resolution { get; set; }
        public List<IElement> Elements { get; private set; } = new List<IElement>();

        /// <summary>
        /// Returns the next Frame as a bitmap byte array
        /// </summary>
        /// <returns></returns>
        public byte[] Render(int index)
        {
            using var surface = SKSurface.Create(info);
            using var canvas = surface.Canvas;
                
            //Clear a Canvas
            canvas.Clear();

            //Draws the elements on the canvas
            foreach (var element in Elements)
            {
                element.DrawOn(index, canvas);
            }

            using var image = surface.Snapshot();

            //returns the canvas as a bmp byte array
            switch (Encoding)
            {
                case Encoding.Png:
                    using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                    {
                        return data.ToArray();
                    }
                default:
                    SKBitmap bitmap = SKBitmap.FromImage(image);
                    return bitmap.Bytes;
            }
        }
    }
}