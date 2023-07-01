using Skmr.Editor.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Instructions
{
    public interface IInstruction<T>
    {
        public Info Info { get; }
        public void Run();

        public T Input(Medium medium);
        public T Output(Medium medium);
    }
}
