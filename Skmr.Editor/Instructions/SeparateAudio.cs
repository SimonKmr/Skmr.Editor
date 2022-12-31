using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class SeparateAudio : IInstruction<SeparateAudio>
    {
        public Info Info { get; } = new Info();
        public void Run()
        {
            Info.Ffmpeg.Run($"-i {Info.Inputs[0]} -map 0:a:{Track} -c copy {Info.Outputs[0]}");
        }

        public SeparateAudio Input(Medium medium)
        {
            Info.Inputs = Info.Inputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
        public SeparateAudio Output(Medium medium)
        {
            Info.Outputs = Info.Outputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }

        public void ExecuteAll()
        {
            Info.Ffmpeg.Run($"-i {Info.Inputs[0]} -vn -acodec copy {Info.Outputs[0]}");
        }

        
        public SeparateAudio(int track)
        {
            Track = track;
        }
        public int Track { get; }



    }
}
