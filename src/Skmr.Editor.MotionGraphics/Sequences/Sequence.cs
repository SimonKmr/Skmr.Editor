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
            foreach (var element in _elements)
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
        
        public byte[] GetFrame(int frame)
            => RenderFrame(frame,Encoding.Raw);
        
        #region List Interface
        
        public IEnumerator<IElement> GetEnumerator()
                => _elements.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public void Add(IElement item)
            => _elements.Add(item);
        

        public void Clear()
            => _elements.Clear();
        
        public bool Contains(IElement item)
            => _elements.Contains(item);
        
        public void CopyTo(IElement[] array, int arrayIndex)
            => _elements.CopyTo(array, arrayIndex);
        
        public bool Remove(IElement item)
            => _elements.Remove(item);
        
        public int Count { get => _elements.Count; }
        public bool IsReadOnly { get => false; }
        public int IndexOf(IElement item)
            => _elements.IndexOf(item);

        public void Insert(int index, IElement item)
            => _elements.Insert(index, item);

        public void RemoveAt(int index)
            => _elements.RemoveAt(index);
        
        public IElement this[int index]
        {
            get => _elements[index];
            set => _elements[index] = value;
        }
        #endregion


    }
}