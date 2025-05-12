namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class MediaHeader : Atom
    {
        //mdhd
        public Byte Version { get; set; }
        public UInt32 CreationTime { get; set; }
        public UInt32 ModificationTime { get; set; }
        public UInt32 TimeScale { get; set; }
        public UInt32 Duration { get; set; }
        public UInt16 Language { get; set; }
        public UInt16 Quality { get; set; }

        public MediaHeader(byte[] bytes) : base(bytes)
        {
            Type = "mdhd";
            Version = bytes[0];
            CreationTime = bytes.ToUInt32(0);
            ModificationTime = bytes.ToUInt32(4);
            TimeScale = bytes.ToUInt32(8);
            Duration = bytes.ToUInt32(12);
            Language = bytes.ToUInt16(16);
            Quality = bytes.ToUInt16(18);
        }
    }
}
