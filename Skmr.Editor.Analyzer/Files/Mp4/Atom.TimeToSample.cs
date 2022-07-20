namespace Skmr.Editor.Analyzer.Files.Mp4
{
    public partial class Atom
    {
        public class TimeToSample
        {
            public Byte Version { get; set; }
            public UInt32 Entries { get; set; }
            public Entry[] TimeToSampleTable { get; set; }


            public TimeToSample(byte[] bytes)
            {
                Version = bytes[0];
                Entries = BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));

                TimeToSampleTable = new Entry[Entries];
                for (int i = 0; i < Entries; i++)
                {
                    int start = 8 + 8 * i;
                    int end = 8 + 8 * (i + 1);
                    TimeToSampleTable[i] = new Entry(bytes[start..end]);
                }
            }

            public class Entry
            {
                public UInt32 Count { get; set; }
                public UInt32 Duration { get; set; }

                public Entry(byte[] bytes)
                {
                    Count = BitConverter.ToUInt32(Utility.ReverseRange(bytes[0..4]));
                    Duration = BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));
                }
            }
        }
    }
}
