using System.Runtime.InteropServices;

namespace Skmr.Editor.Engine.Codecs.Apis.Rav1e
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rational
    {
        public ulong num;
        public ulong den;
    }
}
