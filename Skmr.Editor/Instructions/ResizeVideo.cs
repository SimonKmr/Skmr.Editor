using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class ResizeVideo : IInstruction<ResizeVideo>
    {

        public Info Info { get; } = new Info();
        public void Run()
        {
            Info.Ffmpeg.Run($"-i {Info.Inputs[0]} -filter:v \"scale={Width}:{Height}\" -codec:a copy {Info.Outputs[0]}");
        }
        
        
        public ResizeVideo(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public int Width { get; }
        public int Height { get; }

        public ResizeVideo Input(Medium medium)
        {
            Info.Inputs.Concat(new Medium[] { medium });
            return this;
        }
        public ResizeVideo Output(Medium medium)
        {
            Info.Outputs.Concat(new Medium[] { medium });
            return this;
        }


    }
}
