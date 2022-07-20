using Skmr.Editor.Instructions.Interfaces;
using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Text;
using static Skmr.Editor.Ffmpeg;

namespace Skmr.Editor.Instructions
{
    public class ConvertVideos
    {
        public Medium[] Inputs { get; set; }
        public Medium[] Outputs { get; set; }
        public Ffmpeg Ffmpeg { get; set; }

        public void Execute()
        {
            for (int i = 0; i < Inputs.Length; i++)
            {
                //old params : -i {Inputs[i].Path} -c copy -bsf:v h264_mp4toannexb -f mpegts {Outputs[i].Path}
                Ffmpeg.Run($"-i {Inputs[i].Path} -map 0:v -map 0:a -c copy -bsf:v h264_mp4toannexb -f mpegts {Outputs[i].Path}");
            }

        }
    }
}
