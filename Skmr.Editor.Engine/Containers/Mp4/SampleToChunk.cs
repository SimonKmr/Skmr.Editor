namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class SampleToChunk : Atom
    {
        public Byte Version { get; set; }
        public UInt32 Entries { get; set; }
        public Entry[] SampleToChunkTable { get; set; }


        public SampleToChunk(byte[] bytes)
        {
            Version = bytes[0];
            Entries = BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));

            List<Entry> entries = new List<Entry>();
            for (int i = 0; i < Entries; i++)
            {
                int start = 8 + 12 * i;
                int end = 8 + 12 * (i + 1);
                entries.Add(new Entry(bytes[start..end]));
            }
            SampleToChunkTable = entries.ToArray();
        }

        public class Entry
        {
            public UInt32 FirstChunk { get; set; }
            public UInt32 SamplesPerChunk { get; set; }
            public UInt32 SampleDescriptionId { get; set; }

            public Entry(byte[] bytes)
            {
                FirstChunk = BitConverter.ToUInt32(Utility.ReverseRange(bytes[0..4]));
                SamplesPerChunk = BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));
                SampleDescriptionId = BitConverter.ToUInt32(Utility.ReverseRange(bytes[8..12]));
            }
        }
    }
}
