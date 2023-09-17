using static Skmr.Editor.Engine.Utility;

namespace Skmr.Editor.Engine
{
    public class Image
    {
        private Rgb[,] pixels;

        public int Width { get; }
        public int Height { get; }

        public Image(int width, int height)
        {
            Width = width;
            Height = height;
            pixels = new Rgb[Width, Height];
        }

        public void Set(int x, int y, Rgb rgb)
            => pixels[x, y] = rgb;
        public Rgb Get(int x, int y)
            => pixels[x, y];

        public byte[,,] GetByteBgrMap()
        {
            var res = new byte[Width, Height,3];
            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
                {
                    var channels = Get(x, y);
                    res[x, y, 0] = channels.b;
                    res[x, y, 1] = channels.g;
                    res[x, y, 2] = channels.r;
                }
            }
            return res;
        }
    }
}
