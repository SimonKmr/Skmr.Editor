namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class CompositionOffset : Atom
    {
        public Byte Version { get; set; }
        public UInt32 Entries { get; set; }
        public Entry[] TimeToSampleTable { get; set; }


        public CompositionOffset(byte[] bytes) : base(bytes)
        {
            Version = bytes[0];
            Entries = BitConverter.ToUInt32(Utility.ReverseRange(bytes[(base_offset + 4)..(base_offset + 8)]));

            TimeToSampleTable = new Entry[Entries];
            for (int i = 0; i < Entries; i++)
            {
                int start = base_offset + 8 + 8 * i;
                int end = base_offset + 8 + 16 + 8 * (i + 1);
                TimeToSampleTable[i] = new Entry(bytes[start..end]);
            }
        }

        public class Entry
        {
            public UInt32 SampleCount { get; set; }
            public UInt32 CompositionOffset { get; set; }

            public Entry(byte[] bytes)
            {
                SampleCount = bytes.ToUInt32(0,0);
                CompositionOffset = bytes.ToUInt32(4,0);
            }
        }
    }
}
