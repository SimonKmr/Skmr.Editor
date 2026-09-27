using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Renderer;

namespace Skmr.Editor.MotionGraphics.Sequences
{
    public class Sequence : ISequence, IRenderable<RGBA>
    {
        public List<IElement> Elements { get; } = new List<IElement>();
        public readonly int Height;
        public readonly int Width;
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }

        public Sequence(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public Frame<RGBA> GetFrame(int frame)
        {
            var info = new SKImageInfo(this.Width, this.Height);
            using var surface = SKSurface.Create(info);
            using var canvas = surface.Canvas;
            canvas.Clear();

            //Draws the elements on the canvas
            foreach (var element in Elements)
            {
                DateTime s = DateTime.Now;
                element.DrawOn(frame, canvas);
                var t = DateTime.Now - s;
                var sec = t.TotalSeconds;
                //Console.WriteLine($"{frame};{element.GetType().Name};{sec}");
            }

            using var image = surface.Snapshot();

            var bitmap = SKBitmap.FromImage(image).Pixels
                .Select(x => new RGBA(x.Red, x.Green, x.Blue, x.Alpha)).ToArray();

            return new Frame<RGBA>(Width, Height, bitmap);
        }
    }
}