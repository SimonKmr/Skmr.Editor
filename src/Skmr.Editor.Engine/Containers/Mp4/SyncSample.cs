namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class SyncSample : Atom
    {
        public Byte Version { get; set; }
        public UInt32 Entries { get; set; }
        public UInt32[] SyncSampleTable { get; set; }

        public SyncSample(byte[] bytes) : base(bytes)
        {
            Version = bytes[0];
            Entries = bytes.ToUInt32(0);

            SyncSampleTable = new UInt32[Entries];
            for (int i = 0; i < Entries; i++) SyncSampleTable[i] = bytes.ToUInt32(4 + 4 * i);
        }
    }
}
