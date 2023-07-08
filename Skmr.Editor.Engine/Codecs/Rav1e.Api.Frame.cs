using System.Runtime.InteropServices;

namespace Skmr.Editor.Engine.Codecs
{
    public partial class Rav1e
    {
        public partial class Api
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
    }
}
