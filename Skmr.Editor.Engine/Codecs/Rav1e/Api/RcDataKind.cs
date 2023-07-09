namespace Skmr.Editor.Engine.Codecs.Rav1e.Api
{
    public enum RcDataKind
    {
        /// A Rate Control Summary Packet
        ///
        /// It is emitted once, after the encoder is flushed.
        ///
        /// It contains a summary of the rate control information for the
        /// encoding process that just terminated.
        Summary,
        /// A Rate Control Frame-specific Packet
        ///
        /// It is emitted every time a frame is processed.
        ///
        /// The information contained is required to encode its matching
        /// frame in a second pass encoding.
        Frame,
        /// There is no pass data available for now
        ///
        /// This is emitted if `rav1e_rc_receive_pass_data` is called more
        /// often than it should.
        Empty,
    }
}
