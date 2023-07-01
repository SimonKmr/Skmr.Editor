namespace Skmr.Editor.Engine.Containers.Mp4
{
    public partial class Atom
    {
        public class SampleSize
        {
            public Byte Version { get; set; }
            public UInt32 Size { get; set; }
            public UInt32 Entries { get; set; }
            public UInt32[] SampleSizeTable { get; set; }
            
            public SampleSize(byte[] bytes)
            {
                Version = bytes[0];
                Size = BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));
                Entries = BitConverter.ToUInt32(Utility.ReverseRange(bytes[8..12]));

                List<UInt32> sst = new List<UInt32>();
                for(int i = 0; i < Entries; i++) sst.Add(
                    BitConverter.ToUInt32(
                        Utility.ReverseRange(bytes[(12+4*i)..(12+4*(i+1))])));
                SampleSizeTable = sst.ToArray();
            }
        }
    }
}
