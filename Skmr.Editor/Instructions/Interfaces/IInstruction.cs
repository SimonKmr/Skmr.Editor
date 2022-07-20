using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skmr.Editor.Instructions.Interfaces
{
    public interface IInstruction
    {
        public Medium Output { get; set; }
        public Ffmpeg Ffmpeg { get; set; }
        public void Execute();
    }
}
