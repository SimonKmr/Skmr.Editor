using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Skmr.Editor.Ffmpeg;

namespace Skmr.Editor.Instructions
{
    public class ExportScreenshots : IInstruction<ExportScreenshots>
    {
        public Info Info { get; } = new Info();
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
        

        public void Run()
        {
            Info.Outputs[0] = Info.Inputs[0];
            Info.Ffmpeg.Run($"-i {Input} -vf fps={Frames}/{Seconds} {Folder}\\{Info.Inputs[0].Name}_%05d{Format}");
        }

        public ExportScreenshots Input(Medium medium)
        {
            Info.Inputs.Concat(new Medium[] { medium });
            return this;
        }

        public ExportScreenshots Output(Medium medium)
        {
            Info.Outputs.Concat(new Medium[] { medium });
            return this;
        }
    }
}
