using Skmr.Editor.Instructions.Interfaces;
using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class LayerVideos : IInstruction
    {
        public Medium Input { get; set; }
        public Medium Output { get; set; }
        public Ffmpeg Ffmpeg { get; set; }
        public void Execute()
        {
            //Implementation of             
            // https://stackoverflow.com/questions/35269387/ffmpeg-overlay-one-video-onto-another-video

            // the clips have to be scaled to the same size best case -> same size as base video

            string time = Start.TotalSeconds.ToString();
            Ffmpeg.Run($"-i {Input.Path} -i {Overlay.Path} " +
                $"-filter_complex \"" +
                $"[1:v]setpts=PTS+{time}/TB[a];" +
                $"[0:v][a]overlay[out]\" " +
                $"-map [out] -map 0:a " +
                $"-c:v libx264 -crf 18 -pix_fmt yuv420p " +
                $"-c:a copy " +
                $"{Output.Path}");
        }

        
        public Medium Overlay { get; set; }
        public LayerVideos(TimeSpan start)
        {
            Start = start;
        }
        public TimeSpan Start { get; }
        public LayerVideos()
        {
            Start = TimeSpan.Zero;
        }
    }
}
