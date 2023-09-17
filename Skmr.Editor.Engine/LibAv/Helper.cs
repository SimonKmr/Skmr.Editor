using FFmpeg.AutoGen;

namespace Skmr.Editor.Engine.LibAv
{
    public static class Helper
    {
        public static AVPacket ToPackage(this byte[] bytes)
        {
            AVPacket res = new AVPacket
            {

            };
            
            unsafe
            {
                ffmpeg.av_new_packet(&res, 1024);
            }


            return res;
        }

        public static int ThrowExceptionIfError(this int error)
        {
            if (error < 0) throw new ApplicationException();
            return error;
        }

        public static Y4M.Frame ToY4M(this AVFrame avf)
        {
            throw new NotImplementedException();
        }
    }
}
