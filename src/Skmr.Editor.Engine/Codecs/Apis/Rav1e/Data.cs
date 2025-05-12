using System.Runtime.InteropServices;

namespace Skmr.Editor.Engine.Codecs.Apis.Rav1e
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Data
    {
        public IntPtr data;
        public ulong len;
    }
}
