using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class OverlayImage : IInstruction<OverlayImage>
    {
        public Info Info { get; } = new Info();
        public int PosX { get; set; } = 0;
        public int PosY { get; set; } = 0;
        public Medium Overlay { get; set; }
        public void Run()
        {
            //Implementation of             
            // https://stackoverflow.com/questions/35269387/ffmpeg-overlay-one-video-onto-another-video

            // the clips have to be scaled to the same size best case -> same size as base video

            Info.Ffmpeg.Run($"-i {Info.Inputs[0]} " +
                $"-i {Overlay} " +
                $"-filter_complex \"[0:v][1:v] overlay = {PosX}:{PosY}\" " +
                $"-pix_fmt yuv420p -c:a copy " +
                $"{Info.Outputs[0]}");
        }

        public OverlayImage Input(Medium medium)
        {
            Info.Inputs = Info.Inputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
        public OverlayImage Output(Medium medium)
        {
            Info.Outputs = Info.Outputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
    }
}
