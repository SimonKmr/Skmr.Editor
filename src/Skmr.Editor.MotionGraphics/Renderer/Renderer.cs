using SkiaSharp;
using Skmr.Editor.MotionGraphics.Enums;
using Skmr.Editor.MotionGraphics.Sequences;

namespace Skmr.Editor.MotionGraphics.Renderer
{
    public class Renderer(Sequence sequence, Encoding encoding)
    {
        public Sequence Sequence { get; private set; } = sequence;
        public Encoding Encoding { get; private set; } = encoding;
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }
        public int MaxThreads { get; private set; } = 4;
        public Action<int, byte[]> FrameRendered { get; set; } = delegate { };

        public void Render()
        {
            Parallel.For(StartFrame, EndFrame, new ParallelOptions() { MaxDegreeOfParallelism = MaxThreads }, (i, state) =>
            {
                RenderFrame(i);
            });
        }

        public byte[] RenderFrame(int frame)
        {
            var info = new SKImageInfo(Sequence.Width, Sequence.Height);
            using var surface = SKSurface.Create(info);
            using var canvas = surface.Canvas;

            //Clear a Canvas
            canvas.Clear();

            //Draws the elements on the canvas
            foreach (var element in Sequence.Elements)
            {
                DateTime s = DateTime.Now;
                element.DrawOn(frame, canvas);
                var t = DateTime.Now - s;
                var sec = t.TotalSeconds;
                //Console.WriteLine($"{frame};{element.GetType().Name};{sec}");
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
            return result;
        }
    }
}
