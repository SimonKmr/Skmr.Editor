using SkiaSharp;
using Skmr.Editor.Images.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Images
{
    public class Renderer
    {
        public static void Save(
            int width,
            int height, 
            string file, 
            ICommand[] commands)
        {


            var recorder = new SKPictureRecorder();
            var bitmap = new SKBitmap(width, height);
            var canvas = recorder.BeginRecording(SKRect.Create(width,height));




            #region test drawing functionality

            canvas.Clear(SKColor.FromHsl(0, 0, 0, 0));

            #endregion

            foreach (var c in commands)
            {
                c.Bitmap = bitmap;
                c.Width = width;
                c.Height = height;
                c.Draw();
            }

            canvas.DrawBitmap(bitmap, 0, 0);

            var picture = recorder.EndRecording();
            var image = SKImage.FromPicture(picture, new SKSizeI(width, height));
            var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using(var stream = File.OpenWrite(file))
            {
                data.SaveTo(stream);
            }
        }
    }
}
