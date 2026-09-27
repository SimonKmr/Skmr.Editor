using Skmr.Editor.Data.Interfaces;

namespace Skmr.Editor.Data.Colors
{
    public struct RGB(byte r, byte g, byte b) : IColorModel, IDefault<RGB>
    {
        public byte r = r;
        public byte g = g;
        public byte b = b;

        public static RGB GetDefault()
        {
            return new RGB(0, 0, 0);
        }
    }
}
