using Skmr.Editor.Data.Colors;
using System.Numerics;

namespace Skmr.Editor.Data.Colors
{
    public struct RGB
    {
        public RGB(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public byte r;
        public byte g;
        public byte b;
    }
}
