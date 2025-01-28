namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class ColorTable : Atom
    {
        //ctab
        public UInt32 ColorTableSeed { get; set; }
        public UInt16 ColorTableFlags { get; set; }
        public UInt16 ColorTableSize { get; set; }
        public UInt16[] ColorArray { get; set; }

        public ColorTable(byte[] bytes) : base(bytes)
        {
            ColorTableSeed = bytes.ToUInt32(0);
            ColorTableFlags = bytes.ToUInt16(4);
            ColorTableSize = bytes.ToUInt16(6);

            ColorArray = new UInt16[4];
            ColorArray[0] = bytes.ToUInt16(8);
            ColorArray[1] = bytes.ToUInt16(10);
            ColorArray[2] = bytes.ToUInt16(12);
            ColorArray[3] = bytes.ToUInt16(14);
        }
    }
}
