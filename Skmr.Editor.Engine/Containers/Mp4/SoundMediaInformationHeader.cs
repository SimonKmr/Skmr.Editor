namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class SoundMediaInformationHeader : Atom
    {
        public const string Type = "smhd";
        //smhd
        public Byte Version { get; set; }
        public UInt16 Balance { get; set; }

        public SoundMediaInformationHeader(byte[] bytes)
        {
            Version = bytes[0];
            Balance = BitConverter.ToUInt16(Utility.ReverseRange(bytes[4..6]));
        }
    }
}
