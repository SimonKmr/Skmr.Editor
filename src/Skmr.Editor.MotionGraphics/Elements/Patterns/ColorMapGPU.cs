using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.Cuda;
using SkiaSharp;
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics.Attributes;
using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Structs;

namespace Skmr.Editor.MotionGraphics.Patterns
{
    public class ColorMapGPU : IElement
    {
        public IAttribute<AMap> Map { get; set; }
        public IAttribute<RGBA> Color1 { get; set; }
        public IAttribute<RGBA> Color2 { get; set; }
        public Vec2D Resolution { get; set; }

        public void DrawOn(int frame, SKCanvas canvas)
        {
            var res = Resolution;
            var map = Map.GetFrame(frame).value;
            SKBitmap bitmap = new SKBitmap((int)res.x, (int)res.y);


            var c1 = Color1.GetFrame(frame);
            var c2 = Color2.GetFrame(frame);


            using Context context = Context.CreateDefault();
            using Accelerator accelerator = context.CreateCudaAccelerator(0);

            var n = bitmap.Pixels.Length;

            using var bitmapBuffer = accelerator.Allocate1D<SKColor>(new Index1D(n));
            using var mapBuffer = accelerator.Allocate2DDenseX<float>(new Index2D(map.GetLength(0), map.GetLength(1)));

            mapBuffer.CopyFromCPU(map);

            var kernel = accelerator.LoadAutoGroupedStreamKernel<
                Index1D,
                ArrayView<SKColor>,
                ArrayView2D<float, Stride2D.DenseX>,
                RGBA, RGBA, Vec2D>(
                CreateColorMapKernel);

            kernel(
                bitmapBuffer.Extent.ToIntIndex(),
                bitmapBuffer.View,
                mapBuffer.View,
                c1, c2, Resolution);

            bitmap.Pixels = bitmapBuffer.GetAsArray1D();

            canvas.DrawBitmap(bitmap, 0, 0);
        }

        private static void CreateColorMapKernel(Index1D idx, ArrayView<SKColor> bitmap, ArrayView2D<float, Stride2D.DenseX> map, RGBA color1, RGBA color2, Vec2D resolution)
        {
            var i = idx.X;
            var x = idx.X % (int)resolution.x;
            var y = idx.X / (int)resolution.x;

            bitmap[i] = new SKColor(
                (byte)(color1.r * map[x, y] + color2.r * (1 - map[x, y])),
                (byte)(color1.g * map[x, y] + color2.g * (1 - map[x, y])),
                (byte)(color1.b * map[x, y] + color2.b * (1 - map[x, y])),
                (byte)(color1.a * map[x, y] + color2.a * (1 - map[x, y])));

        }
    }
}
