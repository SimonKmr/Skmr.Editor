namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class SoundMedia : Atom
    {
        public UInt16 Version { get; set; }
        public UInt32 Vendor { get; set; }
        public UInt16 NumberOfChannels { get; set; }
        public UInt16 SampleSize { get; set; }
        public UInt16 CompressionId { get; set; }
        public UInt16 PacketSize { get; set; }
        public UInt32 SampleRate { get; set; }

        public SoundMedia(byte[] bytes)
        {
            Version = BitConverter.ToUInt16(Utility.ReverseRange(bytes[0..2]));
            Vendor = BitConverter.ToUInt16(Utility.ReverseRange(bytes[4..8]));
            NumberOfChannels = BitConverter.ToUInt16(Utility.ReverseRange(bytes[8..10]));
            SampleSize = BitConverter.ToUInt16(Utility.ReverseRange(bytes[10..12]));
            CompressionId = BitConverter.ToUInt16(Utility.ReverseRange(bytes[12..14]));
            PacketSize = BitConverter.ToUInt16(Utility.ReverseRange(bytes[14..16]));
            SampleRate = BitConverter.ToUInt32(Utility.ReverseRange(bytes[16..20]));
        }
    }
}
