using System.Runtime.InteropServices;

namespace Skmr.Editor.Engine.Codecs.Rav1e.Api
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Packet
    {
        public IntPtr data;
        public IntPtr len;
        public ulong input_frameno;
        public FrameType frame_type;
        public IntPtr opaque;
        public IntPtr rec;
        public IntPtr source;
    }
}
