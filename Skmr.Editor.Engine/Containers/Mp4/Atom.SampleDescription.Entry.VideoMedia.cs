using System.Text;

namespace Skmr.Editor.Engine.Containers.Mp4
{
    public partial class Atom
    {
        public partial class SampleDescription
        {
            public partial class Entry
            {
                public class VideoMedia
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

                    public VideoMedia(byte[] bytes)
                    {
                        Version = BitConverter.ToUInt16(Utility.ReverseRange(bytes[0..2]));
                        Vendor = BitConverter.ToUInt16(Utility.ReverseRange(bytes[4..8]));
                        TemporalQuality = BitConverter.ToUInt32(Utility.ReverseRange(bytes[8..12]));
                        SpatialQuality = BitConverter.ToUInt32(Utility.ReverseRange(bytes[12..16]));
                        Width = BitConverter.ToUInt16(Utility.ReverseRange(bytes[16..18]));
                        Height = BitConverter.ToUInt16(Utility.ReverseRange(bytes[18..20]));
                        HorizontalResolution = BitConverter.ToUInt32(Utility.ReverseRange(bytes[20..24]));
                        VerticalResolution = BitConverter.ToUInt32(Utility.ReverseRange(bytes[24..28]));
                        DataSize = BitConverter.ToUInt32(Utility.ReverseRange(bytes[28..32]));
                        FrameCount = BitConverter.ToUInt16(Utility.ReverseRange(bytes[32..34]));
                        CompressorName = Encoding.UTF8.GetString(bytes, 34, 4);
                        Depth = BitConverter.ToUInt16(Utility.ReverseRange(bytes[38..40]));
                        ColorTableId = BitConverter.ToUInt16(Utility.ReverseRange(bytes[40..42]));
                    }
                }
            }
        }
    }
}
