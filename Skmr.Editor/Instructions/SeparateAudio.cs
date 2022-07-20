using Skmr.Editor.Instructions.Interfaces;
using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class SeparateAudio : IInstruction
    {
        public Medium Input { get; set; }
        public Medium Output { get; set; }
        public Ffmpeg Ffmpeg { get; set; }
        public void Execute()
        {
            Ffmpeg.Run($"-i {Input.Path} -map 0:a:{Track} -c copy {Output.Path}");
        }

        
        public SeparateAudio(int track)
        {
            Track = track;
        }
        public int Track { get; }

    }
}
