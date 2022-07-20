using Skmr.Editor.Instructions.Interfaces;
using Skmr.Editor.Media;

using System;
using System.Collections.Generic;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class ChangeSpeed : IInstruction
    {
        public Medium Input { get; set; }
        public Medium Output { get; set; }
        public Ffmpeg Ffmpeg { get; set; }
        public void Execute()
        {
            double invertedSpeed = 1 / Speed;
            Ffmpeg.Run($"-i {Input.Path} -r {Framerate} -filter:v \"setpts={invertedSpeed}*PTS\" {Output.Path}");
        }

        
        
        public ChangeSpeed(double speed, int framerate)
        {
            Speed = speed;
            Framerate = framerate;
        }
        public double Speed { get; }
        public int Framerate { get; }


    }
}
