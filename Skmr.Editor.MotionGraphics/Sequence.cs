using SkiaSharp;
using Skmr.Editor.MotionGraphics.Elements;

namespace Skmr.Editor.MotionGraphics
{
    public class Sequence : IDisposable
    {
        private SKCanvas canvas;
        private SKSurface surface;

        public Sequence(int width, int height)
        {
            Resolution = (width, height);
        }

        public (int width, int height) Resolution { get; set; }
        public List<IElement> Elements { get; private set; } = new List<IElement>();

        /// <summary>
        /// Returns the next Frame as a bitmap byte array
        /// </summary>
        /// <returns></returns>
        public byte[] Render(int index)
        {
            //Create a Canvas
            var info = new SKImageInfo(Resolution.width, Resolution.height);
            surface = SKSurface.Create(info);
            canvas = surface.Canvas;

            //Draws the elements on the canvas
            foreach (var element in Elements)
            {
                element.DrawOn(index,canvas);
            }

            //returns the canvas as a bmp byte array
            using var image = surface.Snapshot();
            using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
            {
                return data.ToArray();
            }
            
        }

        public void Dispose()
        {
            surface.Dispose();
        }
    }
}