using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Engine
{
    public class Image
    {
        private (byte, byte, byte)[,] pixels;

        public int Width { get; }
        public int Height { get; }

        public Image(int width, int height)
        {
            Width = width;
            Height = height;
            pixels = new (byte, byte, byte)[Width, Height];
        }

        public void Set(int x, int y, (byte, byte, byte) rgb)
            => pixels[x, y] = rgb;
        public (byte r, byte g, byte b) Get(int x, int y)
            => pixels[x, y];

        public static Image Open(string Path)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Image> OpenAll(string path)
        {
            throw new NotImplementedException();
        }

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
