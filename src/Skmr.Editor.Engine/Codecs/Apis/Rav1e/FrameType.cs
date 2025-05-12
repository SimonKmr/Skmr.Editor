namespace Skmr.Editor.Engine.Codecs.Apis.Rav1e
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
