using Skmr.Editor.Instructions.Interfaces;
using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skmr.Editor.Instructions
{
    public class ConcatVideos : IInstruction
    {
        public Medium[] Input { get; set; }
        public Medium Output { get; set; }
        public Ffmpeg Ffmpeg { get; set; }
        public void Execute2()
        {
            //https://stackoverflow.com/a/11175851
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Input.Length; i++) sb.Append($"-i {Input[i].Path} ");
            sb.Append("-filter_complex \"");
            for (int i = 0; i < Input.Length; i++) sb.Append($"[{i}:v] [{i}:a] ");
            sb.Append($"concat=n={2}:v={VideoTracks}:a={AudioTracks} [v] [a]\" ");
            sb.Append($"-map \"[v]\" -map \"[a]\" ");
            sb.Append($"{Output.Path}");

            string arguments = sb.ToString();
            Ffmpeg.Run(arguments);
        }

        public void Execute()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"-i \"concat:");
            for (int i = 0; i < Input.Length; i++)
            {
                sb.Append(Input[i].Path);
                if (i < Input.Length - 1) sb.Append("|");
            }
            sb.Append($"\" -vcodec copy -acodec copy {Output.Path}");


            string arguments = sb.ToString();
            Ffmpeg.Run(arguments);
        }


        public ConcatVideos(int videoTracks = 1, int audioTracks = 1)
        {
            VideoTracks = videoTracks;
            AudioTracks = audioTracks;
        }
        public int VideoTracks { get; }
        public int AudioTracks { get; }

        //public override void Execute()
        //{
        //    //if input format not .ts throw error

        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("-i \"concat:");

        //    for (int i = 0; i < Inputs.Length; i++)
        //    {
        //        sb.Append($"{Inputs[i].Path}");
        //        if ((i < Inputs.Length - 1)) sb.Append("|");
        //    }
        //    //-vcodec copy -acodec copy
        //    sb.Append($"\" -vcodec copy -acodec copy {Outputs[0].Path}");

        //    Ffmpeg.StartFfmpeg(sb.ToString());
        //    Ffmpeg.WaitTillDone();
        //}


        //public override void Execute()
        //{
        //    string arguments =
        //        $"-i {Inputs[0].Path} -i {Inputs[1].Path} -i {Inputs[2].Path} " +
        //        "-filter_complex \"[0:v] [0:a] [1:v] [1:a] [2:v] [2:a] " +
        //        "concat=n=3:v=1:a=1 [v] [a]\" " +
        //        $"-map \"[v]\" -map \"[a]\" {Outputs[0].Path}";
        //    Ffmpeg.StartFfmpeg(arguments);
        //    Ffmpeg.WaitTillDone();
        //}
    }
}
