namespace Skmr.Editor.Engine.Containers.Mp4
{
    public partial class Atom
    {
        public class TrackDimensions
        {
            //clef
            //prof
            //enof

            public Byte Version { get; set; }
            public UInt32 Width { get; set; }
            public UInt32 Height { get; set; }

            public TrackDimensions(byte[] bytes)
            {
                Version = bytes[0];
                Width = BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));
                Height = BitConverter.ToUInt32(Utility.ReverseRange(bytes[8..12]));
            }
        }
    }
}
