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

        public SoundMedia(byte[] bytes) : base(bytes)
        {
            Version = bytes.ToUInt16(0);
            Vendor = bytes.ToUInt16(2);
            NumberOfChannels = bytes.ToUInt16(4);
            SampleSize = bytes.ToUInt16(6);
            CompressionId = bytes.ToUInt16(8);
            PacketSize = bytes.ToUInt16(10);
            SampleRate = bytes.ToUInt32(12);
        }
    }
}
