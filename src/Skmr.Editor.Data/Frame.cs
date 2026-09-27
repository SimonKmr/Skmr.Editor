using Skmr.Editor.Data.Interfaces;

namespace Skmr.Editor.Data
{
    public class Frame<T> where T : IColorModel
    {
        public int Width { get; }
        public int Height { get; }
        private Memory<T> Pixels { get; }

        public Span<T> GetSpan() => Pixels.Span;

        public Frame(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Pixels = new T[Width * Height];
        }

        public Frame(int width, int height, T[] pixels)
        {
            this.Width = width;
            this.Height = height;

            if (pixels.Length != width * height)
            {
                throw new Exception("Pixel Array does not match Width and Height");
            }

            if (pixels is null)
            {
                throw new NullReferenceException();
            }

            this.Pixels = pixels;
        }
    }
}
