namespace Skmr.Editor.Analyzer.Files.Mp4
{
    public partial class Atom
    {
        public class VideoMediaInformationHeader
        {
            public Byte Version { get; set; }
            public UInt16 GraphicsMode { get; set; }
            public UInt16[] Opcolor { get; set; }

            public VideoMediaInformationHeader(byte[] bytes)
            {
                GraphicsMode = BitConverter.ToUInt16(Utility.ReverseRange(bytes[4..6]));

                Opcolor = new UInt16[3];
                Opcolor[0] = BitConverter.ToUInt16(Utility.ReverseRange(bytes[6..8]));
                Opcolor[1] = BitConverter.ToUInt16(Utility.ReverseRange(bytes[8..10]));
                Opcolor[2] = BitConverter.ToUInt16(Utility.ReverseRange(bytes[10..12]));
            }
        }
    }
}
