namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class MediaHeader : Atom
    {
        public const string Type = "mdhd";
        //mdhd
        public Byte Version { get; set; }
        public UInt32 CreationTime { get; set; }
        public UInt32 ModificationTime { get; set; }
        public UInt32 TimeScale { get; set; }
        public UInt32 Duration { get; set; }
        public UInt16 Language { get; set; }
        public UInt16 Quality { get; set; }

        public MediaHeader(byte[] bytes)
        {
            Version = bytes[0];
            CreationTime =      BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));
            ModificationTime =  BitConverter.ToUInt32(Utility.ReverseRange(bytes[8..12]));
            TimeScale =         BitConverter.ToUInt32(Utility.ReverseRange(bytes[12..16]));
            Duration =          BitConverter.ToUInt32(Utility.ReverseRange(bytes[16..20]));
            Language =          BitConverter.ToUInt16(Utility.ReverseRange(bytes[20..22]));
            Quality =           BitConverter.ToUInt16(Utility.ReverseRange(bytes[22..24]));
        }
    }
}
