namespace Skmr.Editor.Engine
{
    public class Image<T>
    {
        private T[,] pixels;

        public int Width { get; }
        public int Height { get; }

        public Image(int width, int height)
        {
            Width = width;
            Height = height;
            pixels = new T[Width, Height];
        }

        public void Set(int x, int y, T rgb)
            => pixels[x, y] = rgb;
        public T Get(int x, int y)
            => pixels[x, y];
    }
}
