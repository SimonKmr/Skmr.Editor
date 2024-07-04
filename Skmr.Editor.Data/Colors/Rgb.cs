using Skmr.Editor.Data.Colors;
using Skmr.Editor.Data.Interfaces;
using System.Numerics;

namespace Skmr.Editor.Data.Colors
{
    public struct RGB : IDefault<RGB>
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

        public static RGB GetDefault()
        {
            return new RGB(0, 0, 0);
        }
    }
}
