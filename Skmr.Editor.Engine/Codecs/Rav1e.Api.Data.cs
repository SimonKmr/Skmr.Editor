using System.Runtime.InteropServices;

namespace Skmr.Editor.Engine.Codecs
{
    public partial class Rav1e
    {
        public partial class Api
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct Data
            {
                public IntPtr data;
                public UInt64 len;
            }
        }
    }
}
