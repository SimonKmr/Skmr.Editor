using SkiaSharp;
using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Enums;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Skmr.Editor.MotionGraphics.Sequence
{
    public class Sequence : ISequence
    {
        private SKImageInfo info;
        public Action<int, byte[]> FrameRendered { get; set; } = delegate { };
        public int StartFrame { get; set; }
        public int CurrentFrame { get; set; }
        public int EndFrame { get; set; }
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
        public void Render()
        {
            Parallel.For(StartFrame, EndFrame, (i, state) =>
            {
                RenderSingleFrame(i);
            });
        }

        public void RenderSingleFrame(int frame)
        {
            using var surface = SKSurface.Create(info);
            using var canvas = surface.Canvas;

            //Clear a Canvas
            canvas.Clear();

            //Draws the elements on the canvas
            foreach (var element in Elements)
            {
                element.DrawOn(frame, canvas);
            }

            using var image = surface.Snapshot();

            //returns the canvas as a bmp byte array
            byte[] result;
            switch (Encoding)
            {
                case Encoding.Png:
                    using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                    {
                        result = data.ToArray();
                    }
                    break;
                default:
                    SKBitmap bitmap = SKBitmap.FromImage(image);
                    result = bitmap.Bytes;
                    break;
            }

            FrameRendered(frame, result);
        }
    }
}