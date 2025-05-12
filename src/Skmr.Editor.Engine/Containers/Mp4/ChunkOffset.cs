namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class ChunkOffset : Atom
    {
        public Byte Version { get; set; }
        public UInt32 Entries { get; set; }
        public UInt32[] ChunkOffsetTable { get; set; }

        public ChunkOffset(byte[] bytes) : base(bytes)
        {
            Version = bytes[0];
            Entries = bytes.ToUInt32(0);

            ChunkOffsetTable = new UInt32[Entries];
            for (int i = 0; i < Entries; i++) ChunkOffsetTable[i] = bytes.ToUInt32(i*4 + 4);
        }
    }
}
