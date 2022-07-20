using Skmr.Editor.Instructions.Interfaces;
using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class CutVideo : IInstruction
    {
        public Medium Input { get; set; }
        public Medium Output{ get; set; }
        public Ffmpeg Ffmpeg { get; set; }
        public CutVideo((TimeSpan,TimeSpan) directions)
        {
            Directions = directions;
        }

        public (TimeSpan,TimeSpan) Directions { get; }

        public void Execute()
        {
            string format = @"hh\:mm\:ss\.\0";
            Ffmpeg.Run($"-ss {Directions.Item1.ToString(format)} -t {Directions.Item2.ToString(format)} -i {Input.Path} -c copy {Output.Path}");

        }
    }
}
