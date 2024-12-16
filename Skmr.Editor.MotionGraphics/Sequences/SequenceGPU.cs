using Silk.NET.Windowing;
using SkiaSharp;
using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Enums;

namespace Skmr.Editor.MotionGraphics.Sequences
{
    public class SequenceGPU : ISequence
    {
        private IWindow window;

        private GRGlInterface skiaGlInterface = null;
        private GRContext skiaBackendContext = null;
        private GRBackendRenderTarget skiaBackendRenderTarget = null;

        private SKSurface surface;
        private SKCanvas canvas;
        private SKColorType format = SKColorType.Rgba8888;

        public Encoding Encoding { get; set; }
        public int Index { get; set; } = 0;

        public int StartFrame { get; set; }
        public int CurrentFrame { get; set; }
        public int EndFrame { get; set; }

        public Action<int, byte[]> FrameRendered { get; set; } = delegate { };

        public SequenceGPU(int width, int height)
        {

            WindowOptions options = WindowOptions.Default;
            options.Size = new Silk.NET.Maths.Vector2D<int>(width, height);
            window = Window.Create(options);

            window.Load += Window_Load;
            window.Render += Window_Render;
            Resolution = (width, height);
            var info = new SKImageInfo(Resolution.width, Resolution.height);
        }

        private void Window_Render(double deltaTime)
        {

            //Clear a Canvas
            canvas.Clear(new SKColor(0, 0, 0));

            //Draws the elements on the canvas
            if (Index <= EndFrame)
            {
                foreach (var element in Elements)
                {
                    element.DrawOn(Index, canvas);
                }

                //Screenshots
                using var image = surface.Snapshot();

                //returns the canvas as a bmp byte array
                byte[] imgbyte;
                switch (Encoding)
                {
                    case Encoding.Png:
                        using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                        {
                            imgbyte = data.ToArray();
                        }
                        break;
                    default:
                        SKBitmap bitmap = SKBitmap.FromImage(image);
                        imgbyte = bitmap.Bytes;
                        break;
                }

                FrameRendered(Index, imgbyte);
            }

            //Draws controll elements on the canvas
            canvas.DrawText($"{deltaTime} sec", 20, 60, new SKPaint()
            {
                Color = new SKColor(255, 255, 255)
            });
            canvas.DrawText($"{1 / deltaTime} fps", 20, 80, new SKPaint()
            {
                Color = new SKColor(255, 255, 255)
            });
            canvas.DrawText($"current frame: {Index}", 20, 100, new SKPaint()
            {
                Color = new SKColor(255, 255, 255)
            });

            canvas.Flush();

            Index++;
        }

        private void Window_Load()
        {
            // From https://github.com/Redninja106/Skia_Silk/blob/41ed29f4b6c573affbf491944382b8f9e9077760/Program.cs#L38

            skiaGlInterface = GRGlInterface.CreateOpenGl(name =>
            {
                if (window.GLContext.TryGetProcAddress(name, out nint fn))
                    return fn;
                return 0;
            });

            skiaBackendContext = GRContext.CreateGl(skiaGlInterface);
            format = SKColorType.Rgba8888;

            // create a skia backend render target for the window.
            skiaBackendRenderTarget = new GRBackendRenderTarget(
                window.Size.X, window.Size.Y, // window size
                window.Samples ?? 1, window.PreferredStencilBufferBits ?? 16, // 
                new GRGlFramebufferInfo(
                    0, // use the window's framebuffer
                    format.ToGlSizedFormat()
                    )
                );


            surface = SKSurface.Create(skiaBackendContext, skiaBackendRenderTarget, format);
            canvas = surface.Canvas;
        }

        public (int width, int height) Resolution { get; set; }
        public List<IElement> Elements { get; private set; } = new List<IElement>();

        /// <summary>
        /// Returns the next Frame as a bitmap byte array
        /// </summary>
        /// <returns></returns>
        public void Render()
        {
            Index = StartFrame;
            window.Run();
        }

        public void Dispose()
        {
            canvas.Dispose();
            surface.Dispose();
        }
    }
}