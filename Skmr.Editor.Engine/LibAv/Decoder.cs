using FFmpeg.AutoGen;

namespace Skmr.Editor.Engine.LibAv
{
    public unsafe class Decoder : IDisposable
    {
        private readonly AVCodecContext* _pCodecContext;
        private readonly AVFormatContext* _pFormatContext;
        private readonly AVFrame* _pFrame;
        private readonly AVPacket* _pPacket;
        private readonly AVFrame* _receivedFrame;
        private readonly int _streamIndex;

        public Decoder()
        {
            _pFormatContext = ffmpeg.avformat_alloc_context();
            _receivedFrame = ffmpeg.av_frame_alloc();
            _pPacket = ffmpeg.av_packet_alloc();
            _pFrame = ffmpeg.av_frame_alloc();

            AVCodec* codec = ffmpeg.avcodec_find_decoder(AVCodecID.AV_CODEC_ID_H264);
            _pCodecContext = ffmpeg.avcodec_alloc_context3(codec);
        }

        public bool TryDecodeNextFrame(byte[] bytes, out Y4M.Frame frame)
        {
            ffmpeg.av_frame_unref(_pFrame);
            ffmpeg.av_frame_unref(_receivedFrame);
            int error;

            do
            {
                try
                {
                    ffmpeg.av_packet_unref(_pPacket);
                    error = ffmpeg.av_read_frame(_pFormatContext, _pPacket);

                    //If there are no more frames, return false
                    if(error == ffmpeg.AVERROR_EOF)
                    {
                        frame = (*_pFrame).ToY4M();
                        return false;
                    }

                    error.ThrowExceptionIfError();

                    ffmpeg.avcodec_send_packet(_pCodecContext, _pPacket).ThrowExceptionIfError();
                        
                }
                finally
                {
                    ffmpeg.av_packet_unref(_pPacket);
                }

                error = ffmpeg.avcodec_receive_frame(_pCodecContext, _pFrame);
            } while (error == ffmpeg.AVERROR(ffmpeg.EAGAIN));

            error.ThrowExceptionIfError();

            frame = (*_pFrame).ToY4M();

            return true;
        }

        public void Dispose()
        {
            var pFrame = _pFrame;
            ffmpeg.av_frame_free(&pFrame);

            var pPacket = _pPacket;
            ffmpeg.av_packet_free(&pPacket);

            ffmpeg.avcodec_close(_pCodecContext);
            var pFormatContext = _pFormatContext;
            ffmpeg.avformat_close_input(&pFormatContext);
        }


    }
}
