using Skmr.Editor.Instructions.Interfaces;
using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class BackgroundImage : IInstruction
    {
        public Medium Input { get; set; }
        public Medium Image { get; set; }
        public Medium Output { get; set; }
        public Ffmpeg Ffmpeg { get; set; }
        public VerticalAlignment VertAlignment { get; set; } = new VerticalAlignment();
        public void Execute()
        {
            //Implementation of             
            // https://stackoverflow.com/questions/35269387/ffmpeg-overlay-one-video-onto-another-video

            // the clips have to be scaled to the same size best case -> same size as base video

            Ffmpeg.Run($"-loop 1 -i {Image.Path} " +
                $"-i {Input.Path} " +
                $"-filter_complex \"overlay=(W-w)/2:{VertAlignment}:shortest=1,format=yuv420p\" " +
                $"{Output.Path}");
        }

        public class VerticalAlignment
        {
            public static VerticalAlignment Top { get => new VerticalAlignment("0");  }
            public static VerticalAlignment Center { get => new VerticalAlignment("(H-h)/2"); }
            public static VerticalAlignment Bottom { get => new VerticalAlignment("(H-h)"); }


            public string Value { get; set; }
            public VerticalAlignment(string value)
            {
                Value = value;
            }
            public VerticalAlignment()
            {
                Value = VerticalAlignment.Center.Value;
            }
            public override string ToString() => Value;
        }

    }
}
