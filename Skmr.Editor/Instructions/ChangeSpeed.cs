using Skmr.Editor.Media;
using System.Globalization;
using System.Linq;

namespace Skmr.Editor.Instructions
{
    public class ChangeSpeed : IInstruction<ChangeSpeed>
    {
        public Info Info { get; } = new Info();
        public void Run()
        {
            var ci = new CultureInfo("en-US");
            double invertedSpeed = 1 / Speed;
            string arguments = $"-i {Info.Inputs[0]} -filter_complex \"[0:v]setpts={invertedSpeed.ToString(ci)}*PTS[v];[0:a]atempo={Speed.ToString(ci)}[a]\" -map \"[v]\" -map \"[a]\" {Info.Outputs[0]}";
            Info.Ffmpeg.Run(arguments);
        }

        public ChangeSpeed Input(Medium medium)
        {
            Info.Inputs.Concat(new Medium[] { medium });
            return this;
        }
        public ChangeSpeed Output(Medium medium)
        {
            Info.Outputs.Concat(new Medium[] { medium });
            return this;
        }

        public ChangeSpeed(double speed)
        {
            Speed = speed;
        }
        public double Speed { get; }
    }
}
