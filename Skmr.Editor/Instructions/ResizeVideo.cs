using Skmr.Editor.Instructions.Interfaces;
using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class ResizeVideo : IInstruction
    {
        public Medium Input { get; set; }
        public Medium Output { get; set; }
        public Ffmpeg Ffmpeg { get; set; }
        
        
        public void Execute()
        {
            Ffmpeg.Run($"-i {Input.Path} -filter:v \"scale={Width}:{Height}\" -codec:a copy {Output.Path}");
        }
        
        
        public ResizeVideo(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public int Width { get; }
        public int Height { get; }


    }
}
