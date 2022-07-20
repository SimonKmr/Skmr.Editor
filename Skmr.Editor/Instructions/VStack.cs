using Skmr.Editor.Instructions.Interfaces;
using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class VStack : IInstruction
    {
        public Medium[] Input { get; set; }
        public Medium Output { get; set; }
        public Ffmpeg Ffmpeg { get; set; }
        public void Execute()
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < Input.Length; i++) sb.Append($"-i {Input[i].Path} ");
            sb.Append($"-filter_complex \"vstack,format=yuv420p\" -c:v libx264 -crf 18 {Output.Path}");

            string arguments = sb.ToString();
            Ffmpeg.Run(arguments);
        }
    }
}
