using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Skmr.Editor.Analyzer.ComputerVision
{
    public class ImageLoader
    {
        private DirectoryInfo Directory { get; }
        private FileInfo[] Images { get; }
        public string Folder { get; set; }
        public int Length { get => Images.Length; }

        public ImageLoader(string folder)
        {
            Folder = folder;
            Directory = new DirectoryInfo(folder);
            Images = Directory.GetFiles("*.jpg");
        }
        public static Bitmap Load(string path)
        {
            return new Bitmap(path);
        }

        public Bitmap[] Load()
        {
            Bitmap[] result = new Bitmap[Images.Length];
            for (int i = 0; i < Images.Length; i++) result[i] = Load(Images[i].FullName);
            return result;
        }
        public IEnumerable<Bitmap> LazyLoad()
        {
            for (int i = 0; i < Images.Length; i++)
            {
                yield return Load(Images[i].FullName);
            }
        }
        public Bitmap Screenshot()
        {
            var bitmap = new Bitmap(1920, 1080);
            using(var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(0, 0, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
            }
            return bitmap;
        }
    }
}
