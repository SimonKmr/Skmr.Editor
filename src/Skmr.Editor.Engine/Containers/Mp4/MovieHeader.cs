namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class MovieHeader : Atom
    {
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

        public MovieHeader(byte[] bytes) : base(bytes)
        {
            Type = "mvhd";
            Version = bytes[0];
            CreationTime = bytes.ToUInt32(0);
            ModificationTime = bytes.ToUInt32(4);
            TimeScale = bytes.ToUInt32(8);
            Duration = bytes.ToUInt32(12);
            PreferedRate = bytes.ToUInt32(16);
            PreferedVolume = bytes.ToUInt16(20);
            //Ten bytes reserved for use by Apple.
            PreviewTime = bytes.ToUInt32(32);
            PreviewDuration = bytes.ToUInt32(36);
            PosterTime = bytes.ToUInt32(40);
            SelectionTime = bytes.ToUInt32(44);
            SelectionDuration = bytes.ToUInt32(48);
            CurrentTime = bytes.ToUInt32(52);
            NextTrackID = bytes.ToUInt32(56);
        }
    }
}
