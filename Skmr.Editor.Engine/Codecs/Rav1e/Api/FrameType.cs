namespace Skmr.Editor.Engine.Codecs.Rav1e.Api
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
