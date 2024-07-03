namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class SyncSample : Atom
    {
        public Byte Version { get; set; }
        public UInt32 Entries { get; set; }
        public UInt32[] SyncSampleTable { get; set; }

        public SyncSample(byte[] bytes)
        {
            Version = bytes[0];
            Entries = BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));

            SyncSampleTable = new UInt32[Entries];
            for (int i = 0; i < Entries; i++) SyncSampleTable[i] =
                BitConverter.ToUInt32(
                    Utility.ReverseRange(bytes[(8 + 4 * i)..(8 + 4 * (i + 1))]));
        }
    }
}
