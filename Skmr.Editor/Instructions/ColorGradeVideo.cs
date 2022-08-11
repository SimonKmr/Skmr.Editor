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
            var ci = new CultureInfo("en-US");
            StringBuilder sb = new StringBuilder();
            sb.Append($"-i {Input.Path} ");
            sb.Append("-vf eq=");
            sb.Append($"contrast={Contrast.ToString(ci)}:");
            sb.Append($"brightness={Brighness.ToString(ci)}:");
            sb.Append($"saturation={Saturation.ToString(ci)}:");
            sb.Append($"gamma={Gamma.ToString(ci)}:");
            sb.Append($"gamma_r={GammaR.ToString(ci)}:");
            sb.Append($"gamma_g={GammaG.ToString(ci)}:");
            sb.Append($"gamma_b={GammaB.ToString(ci)}:");
            sb.Append($"gamma_weight={GammaWeight.ToString(ci)}");
            sb.Append($" {Output.Path}");

            Ffmpeg.Run(sb.ToString());
        }
    }
}
