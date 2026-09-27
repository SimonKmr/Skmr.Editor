namespace Skmr.Editor.Engine.Y4M
{
    public class Y4MFrame
    {
        public int Width { get; }
        public int Height { get; }

        public int Size
            => Width * Height * 3 / 2;

        private Memory<byte> Data { get; }

        private int YSize => Width * Height;
        private int ChromaSize => YSize / 4;
        private int ChromaWidth => Width / 2;

        public Y4MFrame(int width, int height)
        {
            Width = width;
            Height = height;
            Data = new byte[Size];
        }

        public Y4MFrame(int width, int height, byte[] data)
        {
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            Width = width;
            Height = height;

            if (data.Length != Size)
                throw new ArgumentException(
                    $"Data length ({data.Length}) does not match expected size ({Size}) for a {width}x{height} Y4M frame.");

            Data = data;
        }

        /// <summary>
        /// Returns a defensive copy of the entire underlying buffer (Y+Cb+Cr planes, packed).
        /// </summary>
        public byte[] GetData()
        {
            var result = new byte[Data.Length];
            Data.CopyTo(result);
            return result;
        }

        /// <summary>
        /// Zero-copy access to the whole underlying buffer, for performance-sensitive callers
        /// (e.g. pinning for native interop). Mirrors Frame&lt;T&gt;.GetSpan().
        /// </summary>
        public Span<byte> GetSpan() => Data.Span;

        /// <summary>
        /// Returns a defensive copy of a single plane's bytes.
        /// </summary>
        public byte[] Get(Y4MChannel channel)
            => GetPlaneSpan(channel).ToArray();

        /// <summary>
        /// Zero-copy access to a single plane, for performance-sensitive callers
        /// (e.g. native interop/pinning). Caller must not hold onto this beyond
        /// the frame's lifetime.
        /// </summary>
        public Span<byte> GetPlaneSpan(Y4MChannel channel)
        {
            var (offset, length) = GetPlaneBounds(channel);
            return Data.Span.Slice(offset, length);
        }

        public byte Get(Y4MChannel channel, int x, int y)
            => Data.Span[GetIndex(channel, x, y)];

        public void Set(Y4MChannel channel, int x, int y, byte value)
            => Data.Span[GetIndex(channel, x, y)] = value;

        private int GetIndex(Y4MChannel channel, int x, int y)
        {
            var (planeOffset, planeLength) = GetPlaneBounds(channel);
            int planeWidth = channel == Y4MChannel.Y ? Width : ChromaWidth;

            int localIndex = (y * planeWidth) + x;

            if ((uint)localIndex >= (uint)planeLength)
            {
                throw new ArgumentOutOfRangeException(
                    $"Coordinates ({x},{y}) are out of bounds for the {channel} plane.");
            }

            return planeOffset + localIndex;
        }

        private (int offset, int length) GetPlaneBounds(Y4MChannel channel)
        {
            int ySize = YSize;
            int cbSize = ChromaSize;
            int crSize = ChromaSize;

            switch (channel)
            {
                case Y4MChannel.Y:
                    return (0, ySize);
                case Y4MChannel.Cb:
                    return (ySize, cbSize);
                case Y4MChannel.Cr:
                    return (ySize + cbSize, crSize);
                default:
                    throw new ArgumentOutOfRangeException(nameof(channel), channel, "Unknown Y4M channel.");
            }
        }
    }
}