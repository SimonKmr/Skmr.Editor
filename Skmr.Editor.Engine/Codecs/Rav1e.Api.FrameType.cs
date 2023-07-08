namespace Skmr.Editor.Engine.Codecs
{
    public partial class Rav1e
    {
        public partial class Api
        {
            public enum FrameType
            {
                /// Key frame.
                KEY,
                /// Inter-frame.
                INTER,
                /// Intra-only frame.
                INTRA_ONLY,
                /// Switching frame.
                SWITCH,
            }
        }
    }
}
