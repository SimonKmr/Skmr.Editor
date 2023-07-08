namespace Skmr.Editor.Engine.Codecs
{
    public partial class Rav1e
    {
        public partial class Api
        {
            public enum EncoderStatus
            {
                /// Normal operation.
                Success = 0,
                /// The encoder needs more data to produce an output packet.
                ///
                /// May be emitted by `rav1e_receive_packet` when frame reordering is
                /// enabled.
                NeedMoreData,
                /// There are enough frames in the queue.
                ///
                /// May be emitted by `rav1e_send_frame` when trying to send a frame after
                /// the encoder has been flushed or the internal queue is full.
                EnoughData,
                /// The encoder has already produced the number of frames requested.
                ///
                /// May be emitted by `rav1e_receive_packet` after a flush request had been
                /// processed or the frame limit had been reached.
                LimitReached,
                /// A Frame had been encoded but not emitted yet.
                Encoded,
                /// Generic fatal error.
                Failure = -1,
                /// A frame was encoded in the first pass of a 2-pass encode, but its stats
                /// data was not retrieved with `rav1e_twopass_out`, or not enough stats data
                /// was provided in the second pass of a 2-pass encode to encode the next
                /// frame.
                NotReady = -2,
            }
        }
    }
}
