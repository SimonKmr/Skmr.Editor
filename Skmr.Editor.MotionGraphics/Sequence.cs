using SkiaSharp;
using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Enums;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Skmr.Editor.MotionGraphics
{
    public class Sequence : IDisposable
    {
        public SKCanvas Canvas { get; private set; }
        private SKSurface surface;

        public Encoding Encoding { get; set; }

        public Sequence(int width, int height)
        {
            Resolution = (width, height);
            var info = new SKImageInfo(Resolution.width, Resolution.height);
            surface = SKSurface.Create(info);
            Canvas = surface.Canvas;
        }

        public (int width, int height) Resolution { get; set; }
        public List<IElement> Elements { get; private set; } = new List<IElement>();

        /// <summary>
        /// Returns the next Frame as a bitmap byte array
        /// </summary>
        /// <returns></returns>
        public byte[] Render(int index)
        {
            //Clear a Canvas
            Canvas.Clear();

            //Draws the elements on the canvas
            foreach (var element in Elements)
            {
                element.DrawOn(index, Canvas);
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

        public void Dispose()
        {
            surface.Dispose();
        }
    }
}