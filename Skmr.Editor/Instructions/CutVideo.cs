using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class CutVideo : IInstruction<CutVideo>
    {
        public Info Info { get; } = new Info();
        public CutVideo((TimeSpan,TimeSpan) directions)
        {
            Directions = directions;
        }

        public (TimeSpan,TimeSpan) Directions { get; }

        public void Run()
        {
            string format = @"hh\:mm\:ss\.\0";
            Info.Ffmpeg.Run($"-ss {Directions.Item1.ToString(format)} -t {Directions.Item2.ToString(format)} -i {Info.Inputs[0]} -c copy {Info.Outputs[0]}");

        }

        public CutVideo Input(Medium medium)
        {
            Info.Inputs = Info.Inputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
        public CutVideo Output(Medium medium)
        {
            Info.Outputs = Info.Outputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
    }
}
