using Silk.NET.GLFW;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SkiaSharp;
using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Enums;

namespace Skmr.Editor.MotionGraphics
{
    public class SequenceGPU
    {
        private IWindow window;
        
        private GRGlInterface skiaGlInterface = null;
        private GRContext skiaBackendContext = null;
        private GRBackendRenderTarget skiaBackendRenderTarget = null;
        private GL gl;

        private SKSurface surface;
        private SKCanvas canvas;
        private SKColorType format = SKColorType.Rgba8888;

        public Encoding Encoding { get; set; }
        public int Index { get; set; } = 0;

        public int StartFrame { get; set; }
        public int CurrentFrame { get; set; }
        public int EndFrame { get; set; }
        
        public Action<int,byte[]> FrameRendered { get; set; } = delegate { };

        public SequenceGPU(int width, int height)
        {
            
            WindowOptions options = WindowOptions.Default;
            options.Size = new Silk.NET.Maths.Vector2D<int> (width, height);
            window = Window.Create(options);
            
            window.Load += Window_Load;
            window.Render += Window_Render;
            Resolution = (width, height);
            var info = new SKImageInfo(Resolution.width, Resolution.height);
        }

        private void Window_Render(double deltaTime)
        {
            if (Index >= EndFrame) return;

            //Clear a Canvas
            canvas.Clear(new SKColor(0,0,0));

            foreach (var element in Elements)
            {
                element.DrawOn(Index, canvas);
            }

            //Draws the elements on the canvas
            canvas.DrawText($"{deltaTime} sec",20,20, new SKPaint()
            {
                Color = new SKColor(0, 0, 255)
            });

            using var image = surface.Snapshot();

            var imgbyte = new byte[0];
            
            //returns the canvas as a bmp byte array
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

            FrameRendered(Index,imgbyte);

            canvas.DrawText($"{1 / deltaTime} fps", 20, 40, new SKPaint()
            {
                Color = new SKColor(0, 0, 255)
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
                return (nint)0;
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
            

            surface = SKSurface.Create(skiaBackendContext,skiaBackendRenderTarget,format);
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