namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class ColorTable : Atom
    {
        //ctab
        public UInt32 ColorTableSeed { get; set; }
        public UInt16 ColorTableFlags { get; set; }
        public UInt16 ColorTableSize { get; set; }
        public UInt16[] ColorArray { get; set; }

        public ColorTable(byte[] bytes)
        {
            ColorTableSeed = BitConverter.ToUInt32(Utility.ReverseRange(bytes[0..4]));
            ColorTableFlags = BitConverter.ToUInt16(Utility.ReverseRange(bytes[4..6]));
            ColorTableSize = BitConverter.ToUInt16(Utility.ReverseRange(bytes[6..8]));
                
            ColorArray = new UInt16[4];
            ColorArray[0] = BitConverter.ToUInt16(Utility.ReverseRange(bytes[8..10]));
            ColorArray[1] = BitConverter.ToUInt16(Utility.ReverseRange(bytes[10..12]));
            ColorArray[2] = BitConverter.ToUInt16(Utility.ReverseRange(bytes[12..14]));
            ColorArray[3] = BitConverter.ToUInt16(Utility.ReverseRange(bytes[14..16]));
        }
    }
}
