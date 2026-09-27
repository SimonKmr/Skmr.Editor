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

        public static RGB ToRgb(this YCbCr color)
        {
            double Y = color.y;
            double Cb = color.cb;
            double Cr = color.cr;

            int r = (int)(Y + 1.40200 * (Cr - 0x80));
            int g = (int)(Y - 0.34414 * (Cb - 0x80) - 0.71414 * (Cr - 0x80));
            int b = (int)(Y + 1.77200 * (Cb - 0x80));

            return new RGB(
                (byte)Math.Clamp(r, 0, 255),
                (byte)Math.Clamp(g, 0, 255),
                (byte)Math.Clamp(b, 0, 255)
            );
        }

        public static RGB ToRgb(this YUV color)
        {
            double yd = color.y;
            double ud = color.u - 128;
            double vd = color.v - 128;

            int red = (int)Math.Round(yd + 1.13983 * vd);
            int green = (int)Math.Round(yd - 0.39465 * ud - 0.58060 * vd);
            int blue = (int)Math.Round(yd + 2.03211 * ud);

            return new RGB
            {
                r = (byte)Math.Clamp(red, 0, 255),
                g = (byte)Math.Clamp(green, 0, 255),
                b = (byte)Math.Clamp(blue, 0, 255),
            };
        }

        public static YCbCr ToYCbCr(this RGB color)
        {
            double R = color.r / 255.0;
            double G = color.g / 255.0;
            double B = color.b / 255.0;

            double Y = 0.299 * R + 0.587 * G + 0.114 * B;
            double Cb = -0.169 * R - 0.331 * G + 0.500 * B;
            double Cr = 0.500 * R - 0.419 * G - 0.081 * B;

            return new YCbCr(
                (byte)(Y * 255),
                (byte)((Cb + 0.5) * 255),
                (byte)((Cr + 0.5) * 255)
            );
        }

        public static YCbCr ToYCbCr(this YUV color)
        {
            return new YCbCr
            {
                y = color.y,
                cr = (byte)(color.v - 128),
                cb = (byte)(color.u - 128),
            };
        }

        public static YUV ToYUV(this RGB color)
        {
            double rd = color.r / 255.0;
            double gd = color.g / 255.0;
            double bd = color.b / 255.0;

            int y = (int)Math.Round(0.299 * rd + 0.587 * gd + 0.114 * bd);
            int u = (int)Math.Round(-0.14713 * rd - 0.28886 * gd + 0.436 * bd) + 128;
            int v = (int)Math.Round(0.615 * rd - 0.51498 * gd - 0.10001 * bd) + 128;

            return new YUV
            {
                y = (byte)Math.Clamp(y, 0, 255),
                u = (byte)Math.Clamp(u, 0, 255),
                v = (byte)Math.Clamp(v, 0, 255),
            };
        }

        public static YUV ToYUV(this YCbCr color)
        {
            double yd = color.y;
            double crd = color.cr - 128;
            double cbd = color.cb - 128;

            int yuvY = (int)Math.Round(yd + 1.402 * crd);
            int yuvU = (int)Math.Round(yd - 0.34414 * cbd - 0.71414 * crd);
            int yuvV = (int)Math.Round(yd + 1.772 * cbd);

            return new YUV
            {
                y = (byte)Math.Clamp(yuvY, 0, 255),
                u = (byte)Math.Clamp(yuvU, 0, 255),
                v = (byte)Math.Clamp(yuvV, 0, 255),
            };
        }

        public unsafe static T[] Create<T>(T* ptr, long length) where T : unmanaged
        {
            T[] array = new T[length];
            for (int i = 0; i < length; i++)
                array[i] = ptr[i];
            return array;
        }

        /// <summary>
        /// Builds an RGBA frame from a raw interleaved byte buffer (4 bytes per pixel).
        /// </summary>
        public static Frame<RGBA> RawToImageRGBA(byte[] bytes, int width, int height)
        {
            const int bytesPerPixel = 4;

            if (bytes.Length != width * height * bytesPerPixel)
                throw new ArgumentException("Buffer length does not match width/height for RGBA data.");

            RGBA[] pixels = new RGBA[width * height];
            ReadOnlySpan<byte> data = bytes;

            for (int i = 0; i < pixels.Length; i++)
            {
                int offset = i * bytesPerPixel;
                pixels[i] = new RGBA(data[offset], data[offset + 1], data[offset + 2], data[offset + 3]);
            }

            return new Frame<RGBA>(width, height, pixels);
        }

        /// <summary>
        /// Builds an RGB frame from a raw interleaved byte buffer.
        /// </summary>
        /// <param name="bytesPerPixel">
        /// Stride of the source data - use 3 for tightly packed RGB, or 4 if the source
        /// includes a 4th (e.g. alpha/padding) byte per pixel that should be skipped.
        /// </param>
        public static Frame<RGB> RawToImageRGB(byte[] bytes, int width, int height, int bytesPerPixel = 3)
        {
            if (bytes.Length != width * height * bytesPerPixel)
                throw new ArgumentException("Buffer length does not match width/height/stride for RGB data.");

            RGB[] pixels = new RGB[width * height];
            ReadOnlySpan<byte> data = bytes;

            for (int i = 0; i < pixels.Length; i++)
            {
                int offset = i * bytesPerPixel;
                pixels[i] = new RGB(data[offset], data[offset + 1], data[offset + 2]);
            }

            return new Frame<RGB>(width, height, pixels);
        }
    }
}