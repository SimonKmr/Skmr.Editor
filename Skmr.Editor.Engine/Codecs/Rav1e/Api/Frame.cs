using System.Runtime.InteropServices;

namespace Skmr.Editor.Engine.Codecs.Rav1e.Api
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Frame
    {
        public IntPtr fi;
        public IntPtr frame_type;
        public IntPtr opaque;
        public IntPtr t35_metadata;
    }
}
