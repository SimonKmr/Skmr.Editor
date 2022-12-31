using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class HStack : IInstruction<HStack>
    {
        public Info Info { get; } = new Info();
        public void Run()
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < Info.Inputs.Length; i++) sb.Append($"-i {Info.Inputs[i]} ");
            sb.Append($"-filter_complex \"hstack,format=yuv420p\" -c:v libx264 -crf 18 {Info.Outputs[0]}");

            string arguments = sb.ToString();
            Info.Ffmpeg.Run(arguments);
        }

        public HStack Input(Medium medium)
        {
            Info.Inputs = Info.Inputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
        public HStack Output(Medium medium)
        {
            Info.Outputs = Info.Outputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
    }
}
