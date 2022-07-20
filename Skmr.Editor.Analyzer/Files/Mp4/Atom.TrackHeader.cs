namespace Skmr.Editor.Analyzer.Files.Mp4
{
    public partial class Atom
    {
        public class TrackHeader
        {
            public const string Type = "tkhd";
            //tkhd
            //https://developer.apple.com/library/archive/documentation/QuickTime/QTFF/QTFFChap2/qtff2.html#//apple_ref/doc/uid/TP40000939-CH204-BBCEIDFA
            public Byte Version { get; set; }
            public UInt32 CreationTime { get; set; }
            public UInt32 ModificationTime { get; set; }
            public UInt32 TrackId { get; set; }
            public UInt32 Duration { get; set; }
            public UInt16 Layer { get; set; }
            public UInt16 AlternateGroup { get; set; }
            public UInt16 Volume { get; set; }
            public UInt32 TrackWidth { get; set; }
            public UInt32 TrackHeight { get; set; }

            public TrackHeader(byte[] bytes)
            {
                Version = bytes[0];
                CreationTime =      BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));
                ModificationTime =  BitConverter.ToUInt32(Utility.ReverseRange(bytes[8..12]));
                TrackId =           BitConverter.ToUInt32(Utility.ReverseRange(bytes[12..16]));
                Duration =          BitConverter.ToUInt32(Utility.ReverseRange(bytes[20..24]));
                Layer =             BitConverter.ToUInt16(Utility.ReverseRange(bytes[28..30]));
                AlternateGroup =    BitConverter.ToUInt16(Utility.ReverseRange(bytes[34..36]));
                Volume =            BitConverter.ToUInt16(Utility.ReverseRange(bytes[36..38]));
                TrackWidth =        BitConverter.ToUInt32(Utility.ReverseRange(bytes[74..78])); //Different in the Documentation but this makes more sense
                TrackHeight =       BitConverter.ToUInt32(Utility.ReverseRange(bytes[78..82])); //
            }
        }
    }
}
