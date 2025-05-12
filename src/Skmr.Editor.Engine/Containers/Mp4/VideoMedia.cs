using System.Text;

namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class VideoMedia : Atom
    {
        public UInt16 Version { get; set; }
        public UInt32 Vendor { get; set; }
        public UInt32 TemporalQuality { get; set; }
        public UInt32 SpatialQuality { get; set; }
        public UInt16 Width { get; set; }
        public UInt16 Height { get; set; }
        public UInt32 HorizontalResolution { get; set; }
        public UInt32 VerticalResolution { get; set; }
        public UInt32 DataSize { get; set; }
        public UInt16 FrameCount { get; set; }
        public String CompressorName { get; set; }
        public UInt16 Depth { get; set; }
        public UInt16 ColorTableId { get; set; }

        public VideoMedia(byte[] bytes) : base(bytes)
        {
            Version = bytes.ToUInt16(0);
            Vendor = bytes.ToUInt32(2);
            TemporalQuality = bytes.ToUInt32(6);
            SpatialQuality = bytes.ToUInt32(10);
            Width = bytes.ToUInt16(14);
            Height = bytes.ToUInt16(16);
            HorizontalResolution = bytes.ToUInt32(18);
            VerticalResolution = bytes.ToUInt32(22);
            DataSize = bytes.ToUInt32(26);
            FrameCount = bytes.ToUInt16(30);
            CompressorName = Encoding.UTF8.GetString(bytes, 32 + 12, 4);
            Depth = bytes.ToUInt16(36);
            ColorTableId = bytes.ToUInt16(40);
        }
    }
}
