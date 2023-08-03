using FFmpeg.AutoGen;

namespace Skmr.Editor.Engine.LibAv
{
    public class Decoder
    {
        public Decoder() 
        {
            ffmpeg.RootPath = "Dlls";
            var v = ffmpeg.avcodec_version();
            Console.WriteLine(v);
        }
    }
}
