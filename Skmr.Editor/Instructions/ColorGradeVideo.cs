using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Instructions
{
    public class ColorGradeVideo : IInstruction<ColorGradeVideo>
    {
        public Info Info { get; } = new Info();
        public float Contrast { get; set; } = 1;
        public float Brighness { get; set; } = 0;
        public float Saturation { get; set; } = 1;
        public float Gamma { get; set; } = 1;
        public float GammaR { get; set; } = 1;
        public float GammaG { get; set; } = 1;
        public float GammaB { get; set; } = 1;
        public float GammaWeight { get; set; } = 1;
        

        public void Run()
        {
            var ci = new CultureInfo("en-US");
            StringBuilder sb = new StringBuilder();
            sb.Append($"-i {Info.Inputs[0]} ");
            sb.Append("-vf eq=");
            sb.Append($"contrast={Contrast.ToString(ci)}:");
            sb.Append($"brightness={Brighness.ToString(ci)}:");
            sb.Append($"saturation={Saturation.ToString(ci)}:");
            sb.Append($"gamma={Gamma.ToString(ci)}:");
            sb.Append($"gamma_r={GammaR.ToString(ci)}:");
            sb.Append($"gamma_g={GammaG.ToString(ci)}:");
            sb.Append($"gamma_b={GammaB.ToString(ci)}:");
            sb.Append($"gamma_weight={GammaWeight.ToString(ci)}");
            sb.Append($" {Info.Outputs[0]}");

            Info.Ffmpeg.Run(sb.ToString());
        }

        public ColorGradeVideo Input(Medium medium)
        {
            Info.Inputs = Info.Inputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
        public ColorGradeVideo Output(Medium medium)
        {
            Info.Outputs = Info.Outputs.Concat(new Medium[] { medium }).ToArray();
            return this;
        }
    }
}
