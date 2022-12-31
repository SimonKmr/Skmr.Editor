using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class BackgroundImage : IInstruction<BackgroundImage>
    {
        public Info Info { get; set; } = new Info();
        public Medium Image { get; set; }
        public VerticalAlignment VertAlignment { get; set; } = new VerticalAlignment();
        public void Run()
        {
            //Implementation of             
            // https://stackoverflow.com/questions/35269387/ffmpeg-overlay-one-video-onto-another-video

            // the clips have to be scaled to the same size best case -> same size as base video

            Info.Ffmpeg.Run($"-loop 1 -i {Image} " +
                $"-i {Info.Inputs[0]} " +
                $"-filter_complex \"overlay=(W-w)/2:{VertAlignment}:shortest=1,format=yuv420p\" " +
                $"{Info.Outputs[0]}");
        }

        public BackgroundImage Input(Medium medium)
        {
            Info.Inputs.Concat(new Medium[] { medium });
            return this;
        }
        public BackgroundImage Output(Medium medium)
        {
            Info.Outputs.Concat(new Medium[] { medium })  ;  
            return this;
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
