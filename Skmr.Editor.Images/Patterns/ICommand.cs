using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Images.Patterns
{
    public interface ICommand
    {
        public void Draw();
        public SKBitmap Bitmap { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
