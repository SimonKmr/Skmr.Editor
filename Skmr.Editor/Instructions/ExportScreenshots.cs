using Skmr.Editor.Instructions.Interfaces;
using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Text;
using static Skmr.Editor.Ffmpeg;

namespace Skmr.Editor.Instructions
{
    public class ExportScreenshots : IInstruction
    {
        public Medium Input { get; set; }
        public Medium Output { get; set; }
        public Ffmpeg Ffmpeg { get; set; }
        public ExportScreenshots(int frames, int seconds, string folder, Format.Image format)
        {
            Frames = frames;
            Seconds = seconds;
            Folder = folder;
            Format = format;
        }

        public int Frames { get; }
        public int Seconds { get; }
        public string Folder { get; }
        public Format.Image Format { get; }
        

        public void Execute()
        {
            Output = Input;
            Ffmpeg.Run($"-i {Input.Path} -vf fps={Frames}/{Seconds} {Folder}\\{Input.Name}_%05d{Format}");
        }
    }
}
