using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class SeparateVideo : IInstruction<SeparateVideo>
    {
        public Info Info { get; } = new Info();
        public void Run()
        {
            Info.Ffmpeg.Run($"-i {Info.Inputs[0]} -map 0:v -c copy {Info.Outputs[0]}");
        }

        public SeparateVideo Input(Medium medium)
        {
            Info.Inputs.Concat(new Medium[] { medium });
            return this;
        }
        public SeparateVideo Output(Medium medium)
        {
            Info.Outputs.Concat(new Medium[] { medium });
            return this;
        }
    }
}
