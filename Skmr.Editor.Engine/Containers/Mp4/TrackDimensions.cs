namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class TrackDimensions : Atom
    {
        //clef
        //prof
        //enof

        public Byte Version { get; set; }
        public UInt32 Width { get; set; }
        public UInt32 Height { get; set; }

        public TrackDimensions(byte[] bytes) : base(bytes)
        {
            Version = bytes[0];
            Width = bytes.ToUInt32(0);
            Height = bytes.ToUInt32(4);
        }
    }
}
