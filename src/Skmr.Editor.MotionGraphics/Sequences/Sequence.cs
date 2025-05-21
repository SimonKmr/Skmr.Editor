using Newtonsoft.Json;
using SkiaSharp;
using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Enums;

namespace Skmr.Editor.MotionGraphics.Sequences
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Sequence : ISequence
    {
        private readonly SKImageInfo info;
        public Action<int, byte[]> FrameRendered { get; set; } = delegate { };
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }
        public int MaxThreads { get; set; } = 4;
        public Encoding Encoding { get; set; }

        public Sequence(int width, int height)
        {
            Resolution = (width, height);
            info = new SKImageInfo(Resolution.width, Resolution.height);
        }

        public (int width, int height) Resolution { get; set; }
        [JsonProperty] public List<IElement> Elements { get; } = new List<IElement>();

        /// <summary>
        /// Returns the next Frame as a bitmap byte array
        /// </summary>
        /// <returns></returns>
        public void Render()
        {
            Parallel.For(StartFrame, EndFrame, new ParallelOptions() { MaxDegreeOfParallelism = MaxThreads }, (i, state) =>
            {
                RenderFrame(i);
            });
        }

        public byte[] RenderFrame(int frame)
        {
            using var surface = SKSurface.Create(info);
            using var canvas = surface.Canvas;

            //Clear a Canvas
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