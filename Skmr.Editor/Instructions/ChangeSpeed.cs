using Skmr.Editor.Instructions.Interfaces;
using Skmr.Editor.Media;
using System.Globalization;

namespace Skmr.Editor.Instructions
{
    public class ChangeSpeed : IInstruction
    {
        public Medium Input { get; set; }
        public Medium Output { get; set; }
        public Ffmpeg Ffmpeg { get; set; }
        
        
        public void Execute()
        {
            var ci = new CultureInfo("en-US");
            double invertedSpeed = 1 / Speed;
            string arguments = $"-i {Input.Path} -filter_complex \"[0:v]setpts={invertedSpeed.ToString(ci)}*PTS[v];[0:a]atempo={Speed.ToString(ci)}[a]\" -map \"[v]\" -map \"[a]\" {Output.Path}";
            Ffmpeg.Run(arguments);
        }
        
        public ChangeSpeed(double speed)
        {
            Speed = speed;
        }
        public double Speed { get; }
    }
}
