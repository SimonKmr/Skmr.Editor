using System.Runtime.InteropServices;

namespace Skmr.Editor.Engine.Rav1e.Api
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rational
    {
        public ulong num;
        public ulong den;
    }
}
