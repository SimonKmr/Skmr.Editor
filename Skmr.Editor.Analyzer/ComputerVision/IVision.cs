using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.Engine;

namespace Skmr.Editor.Analyzer.ComputerVision
{
    public interface IVision
    {
        public Feature[] Detect(Image<RGB> image);
    }
}
