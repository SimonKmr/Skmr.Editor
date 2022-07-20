using Skmr.Editor.Instructions.Interfaces;
using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class OverlayImage : IInstruction
    {
        public int PosX { get; set; } = 0;
        public int PosY { get; set; } = 0;
        public Medium Input { get; set; }
        public Medium Overlay { get; set; }
        public Medium Output { get; set; }
        public Ffmpeg Ffmpeg { get; set; }
        public void Execute()
        {
            //Implementation of             
            // https://stackoverflow.com/questions/35269387/ffmpeg-overlay-one-video-onto-another-video

            // the clips have to be scaled to the same size best case -> same size as base video

            Ffmpeg.Run($"-i {Input.Path} " +
                $"-i {Overlay.Path} " +
                $"-filter_complex \"[0:v][1:v] overlay = {PosX}:{PosY}\" " +
                $"-pix_fmt yuv420p -c:a copy " +
                $"{Output.Path}");
        }
    }
}
