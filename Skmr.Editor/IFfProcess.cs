using System.Diagnostics;

namespace Skmr.Editor
{
    public interface IFfProcess
    {
        public Process Process { get; }
        public string Executable { get; set; }
        public void Run(string args);
    }
}