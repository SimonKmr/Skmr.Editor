using System;
using static Skmr.Editor.Ffmpeg;
using io = System.IO;

namespace Skmr.Editor.Media
{
    public class Medium
    {
        public string Folder { get; set; }
        public string Name { get; set; }
        public Format Format { get; set; }
        public string File { get => $"{Name}{Format}"; }
        public string Path { get => $"{Folder}\\{File}"; }

        public Medium(string folder, string name, Format format)
        {
            Folder = folder;
            Name = name;
            Format = format;
        }
        public Medium Copy(string to)
        {
            string res = $"{to}\\{File}";
            io.File.Copy(Path, res);
            return new Medium(to,Name,Format);
        }
        public void Move(string to)
        {
            string res = $"{to}\\{File}";
            io.File.Move(Path, res);
            Folder = to;
        }
        public void Delete()
        {
            io.File.Delete(Path);
        }
        public bool Exists()
        {
            return io.File.Exists(Path);
        }

        public static Medium GenerateMedium(string folder, Format format)
            => new Medium(folder, DateTime.Now.Ticks.ToString(), format);
    }
}
