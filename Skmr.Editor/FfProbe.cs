using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Skmr.Editor
{
    internal class FfProbe : IFfProcess
    {
        public Process Process { get; private set; }
        public string Executable { get; set; }
        public FfProbe()
        {
            Executable = @"Dependencies\ffmpeg-20200814-a762fd2-win64-static\bin\ffprobe.exe";
        }
        public void Run(string args)
        {
            if (Process == null)
            {
                Process = Process.Start(Executable, args);
                Output = Process.StandardOutput.ReadToEnd();
            }
            if (Process.HasExited)
            {
                Process = Process.Start(Executable, args);
                Output = Process.StandardOutput.ReadToEnd();
            }
            throw new Exception("Process already running");
        }
        public string Output { get; private set; }
        public void Dispose()
        {
            Process.Kill();
        }
    }
}
