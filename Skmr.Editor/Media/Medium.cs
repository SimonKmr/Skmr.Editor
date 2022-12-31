using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using static Skmr.Editor.Ffmpeg;
using io = System.IO;

namespace Skmr.Editor.Media
{
    public class Medium: IEquatable<Medium>, ICloneable
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
        public Medium(string path) 
        {
            var file = new FileInfo(path);
            var nameParts = file.Name.Split('.');
            var extension = nameParts[nameParts.Length-1];

            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < nameParts.Length - 1; i++)
                sb.Append(nameParts[i]);

            Folder = file.Directory.FullName;
            Name = sb.ToString();
            Format = Format.Extensions["."+extension];
            
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
            => new(folder, DateTime.Now.Ticks.ToString(), format);

        public override string ToString()
            => Path;
        
        public bool Equals(Medium other)
            => Path.Equals(other.Path);

        public object Clone()
            => new Medium(Folder, Name, Format);

    }
}
