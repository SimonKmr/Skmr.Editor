using System.Collections;
using System.IO.MemoryMappedFiles;
using Newtonsoft.Json;
using SkiaSharp;
using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Enums;

namespace Skmr.Editor.MotionGraphics.Sequences
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Sequence : ISequence
    {
        private SKImageInfo info;
        private bool isLoaded = false;
        private MemoryMappedViewAccessor _accessor;
        public Action<int, byte[]> FrameRendered { get; set; } = delegate { };
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }
        public bool HasSharedMemory { get; }
        public int MaxThreads { get; set; } = 4;

        [JsonConstructor]
        private Sequence()
        {
            isLoaded = true;
        }
        public Sequence(int width, int height, bool hasSharedMemory = false)
        {
            Resolution = (width, height);
            info = new SKImageInfo(Resolution.width, Resolution.height);

            HasSharedMemory = hasSharedMemory;
            if (HasSharedMemory)
            {
                int bufferSize = width * height * 4;
                var mmf = MemoryMappedFile.CreateOrOpen("frame_buffer", bufferSize);
                _accessor = mmf.CreateViewAccessor();
            }
        }

        [JsonProperty] public (int width, int height) Resolution { get; set; }
        [JsonProperty] private List<IElement> _elements = new();

        /// <summary>
        /// Returns the next Frame as a bitmap byte array
        /// </summary>
        /// <returns></returns>
        public void Render(Encoding encoding)
        {
            Parallel.For(StartFrame, EndFrame, new ParallelOptions() { MaxDegreeOfParallelism = MaxThreads }, (i, state) =>
            {
                RenderFrame(i,encoding);
            });
        }

        public byte[] RenderFrame(int frame, Encoding encoding)
        {
            if (isLoaded)
            {
                info = new SKImageInfo(Resolution.width, Resolution.height);
                isLoaded = false;
            }
            using var surface = SKSurface.Create(info);
            using var canvas = surface.Canvas;

            //Clear a Canvas
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

            //returns the canvas as a bmp byte array
            byte[] result;
            switch (encoding)
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

            if (HasSharedMemory)
            {
                _accessor.WriteArray(0, result, 0, result.Length);
            }

            FrameRendered(frame, result);
            return result;
        }
        
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