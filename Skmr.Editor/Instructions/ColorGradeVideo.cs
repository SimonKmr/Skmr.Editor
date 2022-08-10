using Skmr.Editor.Instructions.Interfaces;
using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Instructions
{
    public class ColorGradeVideo : IInstruction
    {
        public float Contrast { get; set; } = 1;
        public float Brighness { get; set; } = 0;
        public float Saturation { get; set; } = 1;
        public float Gamma { get; set; } = 1;
        public float GammaR { get; set; } = 1;
        public float GammaG { get; set; } = 1;
        public float GammaB { get; set; } = 1;
        public float GammaWeight { get; set; } = 1;
        
        public Medium Input { get; set; }
        public Medium Output { get; set; }
        public Ffmpeg Ffmpeg { get; set; }

        public void Execute()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"-i {Input.Path} ");
            sb.Append("-vf eq=");
            sb.Append($"contrast={Contrast.ToString(new CultureInfo("en-US"))}:");
            sb.Append($"brightness={Brighness.ToString(new CultureInfo("en-US"))}:");
            sb.Append($"saturation={Saturation.ToString(new CultureInfo("en-US"))}:");
            sb.Append($"gamma={Gamma.ToString(new CultureInfo("en-US"))}:");
            sb.Append($"gamma_r={GammaR.ToString(new CultureInfo("en-US"))}:");
            sb.Append($"gamma_g={GammaG.ToString(new CultureInfo("en-US"))}:");
            sb.Append($"gamma_b={GammaB.ToString(new CultureInfo("en-US"))}:");
            sb.Append($"gamma_weight={GammaWeight.ToString(new CultureInfo("en-US"))}");
            sb.Append($" {Output.Path}");

            Ffmpeg.Run(sb.ToString());
        }
    }
}
