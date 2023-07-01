using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Skmr.Editor
{
    public partial class Ffmpeg : IFfProcess
    {
        public Process Process { get; private set; }
        public string Executable { get; set; }

        
        public bool HideBanner { get; set; }
        public bool HideWindow { get; set; }
        public logLevel LogLevel { get; set; } = logLevel.info;
        public Overwrite OverrideState { get; set; } = Overwrite.ask;
        private string CreateDefaultArgs()
        {
            StringBuilder sb = new StringBuilder();
            if (HideBanner) sb.Append("-hide_banner ");
            switch (LogLevel)
            {
                case logLevel.quiet:    sb.Append("-loglevel quiet ");   break;
                case logLevel.panic:    sb.Append("-loglevel panic ");   break;
                case logLevel.fatal:    sb.Append("-loglevel fatal ");   break;
                case logLevel.error:    sb.Append("-loglevel error ");   break;
                case logLevel.warning:  sb.Append("-loglevel warning "); break;
                case logLevel.info:     sb.Append("-loglevel info ");    break;
                case logLevel.verbose:  sb.Append("-loglevel verbose "); break;
                case logLevel.debug:    sb.Append("-loglevel debug ");   break;
                case logLevel.trace:    sb.Append("-loglevel trace ");   break;
            }
            switch (OverrideState)
            {
                case Overwrite.overwriting: sb.Append("-y "); break;
                case Overwrite.skip:        sb.Append("-n "); break;
                case Overwrite.ask: break;
            }
            return sb.ToString();
        }
        
        public Ffmpeg() 
        {
            string ffmpegPath = Environment.GetEnvironmentVariable("ffmpeg", EnvironmentVariableTarget.User) + "\\ffmpeg.exe";
            Executable = ffmpegPath ?? throw new Exception("environment variable \"ffmpeg\" not set");
        }
        public Ffmpeg(string executable)
        {
            Executable = executable;
        }

        //Ffmpeg Methods
        public void Run(string args)
        {
            if (Process == null)
            {
                RunFfmpeg(args);
                return;
            }
            if (Process.HasExited)
            {
                RunFfmpeg(args);
                return;
            }
            throw new Exception("Process already running");
        }

        private void RunFfmpeg(string args)
        {
            var psi = new ProcessStartInfo(Executable);
            psi.CreateNoWindow = HideWindow;
            psi.Arguments = $"{CreateDefaultArgs()}{args}";
            Process = Process.Start(psi);
            Process.WaitForExit();
        }

        public void Dispose()
        {
            Process.Kill();
        }
    }
}
