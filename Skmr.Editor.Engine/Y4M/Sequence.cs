using System.Collections;

namespace Skmr.Editor.Engine.Y4M
{
    public class Sequence : IList<Frame>
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int FrameRate { get; set; }
        public string ColorSpace { get; set; }
        private List<Frame> Frames { get; set; }


        public Sequence(int frameRate)
        {
            FrameRate = frameRate;
            ColorSpace = "4:2:0";
            Frames = new List<Frame>();
        }

        public Frame this[int index] 
        { 
            get => Frames[index]; set 
            {
                Frames[index] = value;
            } 
        }

        public int Count => Frames.Count;

        public bool IsReadOnly => false;

        public int IndexOf(Frame item)
            => Frames.IndexOf(item);

        public void Insert(int index, Frame item)
            => Frames.Insert(index, item);

        public void RemoveAt(int index)
            => Frames.RemoveAt(index);

        public void Add(Frame item)
        {
            if (Width != item.Width || Height != item.Height)
                throw new Exception();

            Frames.Add(item);
        }

        public void Clear()
            => Frames.Clear();

        public bool Contains(Frame item)
            => Frames.Contains(item);

        public void CopyTo(Frame[] array, int arrayIndex)
            => Frames.CopyTo(array, arrayIndex);

        public bool Remove(Frame item)
            => Frames.Remove(item);

        public IEnumerator<Frame> GetEnumerator()
            => Frames.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => Frames.GetEnumerator();
    } 
}
