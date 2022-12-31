using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Instructions
{
    public class Info
    {
        public Medium[] Inputs { get; internal set; } = new Medium[0];
        public Medium[] Outputs { get; internal set; } = new Medium[0];
        public Ffmpeg Ffmpeg { get; set; } = Ffmpeg.Manager.Ffmpeg;
    }
}
