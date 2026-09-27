using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.Engine.Y4M;

namespace Skmr.Editor.Engine
{
    public static partial class Utility
    {
        /// <summary>
        /// Converts an RGB frame to a Y4M (YUV 4:2:0 planar) frame.
        /// Chroma is point-sampled (not averaged) at 2x2 block origins.
        /// </summary>
        static public Y4MFrame ToY4MFrame(this Frame<RGB> image)
        {
            int width = image.Width;
            int height = image.Height;

            int ySize = width * height;
            int cbSize = ySize / 4;
            int crSize = ySize / 4;

            byte[] data = new byte[ySize + cbSize + crSize];
            Span<byte> dataSpan = data;

            Span<byte> yBytes = dataSpan.Slice(0, ySize);
            Span<byte> cbBytes = dataSpan.Slice(ySize, cbSize);
            Span<byte> crBytes = dataSpan.Slice(ySize + cbSize, crSize);

            ReadOnlySpan<RGB> pixels = image.GetSpan();

            // Y plane - full resolution
            for (int y = 0; y < height; y++)
            {
                int rowOffset = y * width;
                for (int x = 0; x < width; x++)
                {
                    yBytes[rowOffset + x] = pixels[rowOffset + x].ToYCbCr().y;
                }
            }

            // Cb/Cr planes - subsampled 4:2:0 (point sample at each 2x2 block origin)
            int chromaWidth = width / 2;
            int chromaHeight = height / 2;

            for (int cy = 0; cy < chromaHeight; cy++)
            {
                int chromaRowOffset = cy * chromaWidth;
                int sourceRowOffset = (cy * 2) * width;

                for (int cx = 0; cx < chromaWidth; cx++)
                {
                    RGB c = pixels[sourceRowOffset + (cx * 2)];
                    var yCbCr = c.ToYCbCr();

                    int index = chromaRowOffset + cx;
                    cbBytes[index] = yCbCr.cb;
                    crBytes[index] = yCbCr.cr;
                }
            }

            return new Y4MFrame(width, height, data);
        }

        /// <summary>
        /// Converts a Y4M (YUV 4:2:0 planar) frame back to an RGB frame.
        /// Chroma is upsampled by nearest-neighbor (matches the point-sampling used in ToY4MFrame).
        /// </summary>
        static public Frame<RGB> ToImage(this Y4MFrame y4m)
        {
            int width = y4m.Width;
            int height = y4m.Height;
            byte[] frameData = y4m.GetData();

            int ySize = width * height;
            int cbSize = ySize / 4;
            int crSize = ySize / 4;

            ReadOnlySpan<byte> data = frameData;
            ReadOnlySpan<byte> yComponent = data.Slice(0, ySize);
            ReadOnlySpan<byte> cbComponent = data.Slice(ySize, cbSize);
            ReadOnlySpan<byte> crComponent = data.Slice(ySize + cbSize, crSize);

            int chromaWidth = width / 2;

            RGB[] pixels = new RGB[width * height];

            for (int y = 0; y < height; y++)
            {
                int rowOffset = y * width;
                int chromaRowOffset = (y / 2) * chromaWidth;

                for (int x = 0; x < width; x++)
                {
                    int chromaIndex = chromaRowOffset + (x / 2);

                    var yCbCr = new YCbCr(
                        yComponent[rowOffset + x],
                        cbComponent[chromaIndex],
                        crComponent[chromaIndex]);

                    pixels[rowOffset + x] = yCbCr.ToRgb();
                }
            }

            return new Frame<RGB>(width, height, pixels);
        }

        public unsafe static T[] Create<T>(T* ptr, long length) where T : unmanaged
        {
            T[] array = new T[length];
            for (int i = 0; i < length; i++)
                array[i] = ptr[i];
            return array;
        }
    }
}