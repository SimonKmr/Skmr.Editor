using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Skmr.Editor.Ffmpeg;

namespace Skmr.Editor.Instructions
{
    public class ConvertVideos : IInstruction<ConvertVideos>
    {
        public Info Info { get; } = new Info();
        public void Run()
        {
            for (int i = 0; i < Info.Inputs.Length; i++)
            {
                //old params : -i {Inputs[i].Path} -c copy -bsf:v h264_mp4toannexb -f mpegts {Outputs[i].Path}
                Info.Ffmpeg.Run($"-i {Info.Inputs[i]} -map 0:v -map 0:a -c copy -bsf:v h264_mp4toannexb -f mpegts {Info.Outputs[i]}");
            }

        }

        public ConvertVideos Input(Medium medium)
        {
            Info.Inputs.Concat(new Medium[] { medium });
            return this;
        }
        public ConvertVideos Output(Medium medium)
        {
            Info.Outputs.Concat(new Medium[] { medium });
            return this;
        }
    }
}
