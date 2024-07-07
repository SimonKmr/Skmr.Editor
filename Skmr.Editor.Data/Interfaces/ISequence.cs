using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Data.Interfaces
{
    public interface ISequence
    {
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }

        public void Render();
    }
}
