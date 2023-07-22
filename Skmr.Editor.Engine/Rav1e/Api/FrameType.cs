namespace Skmr.Editor.Engine.Rav1e.Api
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
