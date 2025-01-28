namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class VideoMediaInformationHeader : Atom
    {
        public Byte Version { get; set; }
        public UInt16 GraphicsMode { get; set; }
        public UInt16[] Opcolor { get; set; }

        public VideoMediaInformationHeader(byte[] bytes) : base(bytes)
        {
            GraphicsMode = bytes.ToUInt16(0);

            Opcolor = new UInt16[3];
            Opcolor[0] = bytes.ToUInt16(2);
            Opcolor[1] = bytes.ToUInt16(4);
            Opcolor[2] = bytes.ToUInt16(6);
        }
    }
}
