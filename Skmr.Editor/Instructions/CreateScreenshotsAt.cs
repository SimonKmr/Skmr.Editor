using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Skmr.Editor.Ffmpeg;

namespace Skmr.Editor.Instructions
{
    public class CreateScreenshotAt : IInstruction<CreateScreenshotAt>
    {
        public Info Info { get; } = new Info();
        public CreateScreenshotAt(TimeSpan time, Format format)
        {
            Time = time;
            Format = format;
        }

        public TimeSpan Time { get; }
        public Format Format { get; }

        public void Run()
        {
            Info.Ffmpeg.Run($"-i {Info.Inputs[0]} -ss {Time} -frames:v 1 {Info.Outputs[0]}");
        }

        public CreateScreenshotAt Input(Medium medium)
        {
            Info.Inputs.Concat(new Medium[] { medium });
            return this;
        }
        public CreateScreenshotAt Output(Medium medium)
        {
            Info.Outputs.Concat(new Medium[] { medium });
            return this;
        }
    }
}
