using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor
{
    public partial class Ffmpeg
    {
        public class Manager
        {
            public static Ffmpeg Ffmpeg { get; set; } = new Ffmpeg();
        }
    }
}
