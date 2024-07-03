namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class MovieHeader : Atom
    {
        public const string Type = "mvhd";
        //mvhd
        public Byte Version { get; set; }
        public UInt32 CreationTime { get; set; }
        public UInt32 ModificationTime { get; set; }
        public UInt32 TimeScale { get; set; }
        public UInt32 Duration { get; set; }
        public UInt32 PreferedRate { get; set; }
        public UInt16 PreferedVolume { get; set; }
        public UInt32 PreviewTime { get; set; }
        public UInt32 PreviewDuration { get; set; }
        public UInt32 PosterTime { get; set; }
        public UInt32 SelectionTime { get; set; }
        public UInt32 SelectionDuration { get; set; }
        public UInt32 CurrentTime { get; set; }
        public UInt32 NextTrackID { get; set; }

        public MovieHeader(byte[] bytes)
        {
            Version = bytes[0];
            CreationTime =      BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));
            ModificationTime =  BitConverter.ToUInt32(Utility.ReverseRange(bytes[8..12]));
            TimeScale =         BitConverter.ToUInt32(Utility.ReverseRange(bytes[12..16]));
            Duration =          BitConverter.ToUInt32(Utility.ReverseRange(bytes[16..20]));
            PreferedRate =      BitConverter.ToUInt32(Utility.ReverseRange(bytes[20..24]));
            PreferedVolume =    BitConverter.ToUInt16(Utility.ReverseRange(bytes[24..26]));
            PreviewTime =       BitConverter.ToUInt32(Utility.ReverseRange(bytes[72..76]));
            PreviewDuration =   BitConverter.ToUInt32(Utility.ReverseRange(bytes[76..80]));
            PosterTime =        BitConverter.ToUInt32(Utility.ReverseRange(bytes[80..84]));
            SelectionTime =     BitConverter.ToUInt32(Utility.ReverseRange(bytes[84..88]));
            SelectionDuration = BitConverter.ToUInt32(Utility.ReverseRange(bytes[88..92]));
            CurrentTime =       BitConverter.ToUInt32(Utility.ReverseRange(bytes[92..96]));
            NextTrackID =       BitConverter.ToUInt32(Utility.ReverseRange(bytes[96..100]));
        }
    }
}
