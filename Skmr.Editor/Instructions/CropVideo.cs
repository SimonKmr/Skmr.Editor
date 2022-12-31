using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class CropVideo : IInstruction<CropVideo>
    {
        public Info Info { get; } = new Info();
        public CropVideo(int width, int height, int x = 0, int y = 0)
        {
            Width = width;
            Height = height;
            X = x;
            Y = y;
        }

        public int Width { get; }
        public int Height { get; }
        public int X { get; }
        public int Y { get; }

        public void Run()
        {
            Info.Ffmpeg.Run($"-i {Info.Inputs[0]} -filter:v \"crop={Width}:{Height}:{X}:{Y}\" -codec:a copy {Info.Outputs[0]}");
        }

        public CropVideo Input(Medium medium)
        {
            Info.Inputs.Concat(new Medium[] { medium });
            return this;
        }
        public CropVideo Output(Medium medium)
        {
            Info.Outputs.Concat(new Medium[] { medium });
            return this;
        }
    }
}
