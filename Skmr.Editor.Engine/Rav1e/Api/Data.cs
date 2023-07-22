using System.Runtime.InteropServices;

namespace Skmr.Editor.Engine.Codecs.Api
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Data
    {
        public IntPtr data;
        public UInt64 len;
    }
}
