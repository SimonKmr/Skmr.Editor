using System.Runtime.InteropServices;

namespace Skmr.Editor.Engine.Codecs.Rav1e.Api
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Data
    {
        public IntPtr data;
        public UInt64 len;
    }
}
