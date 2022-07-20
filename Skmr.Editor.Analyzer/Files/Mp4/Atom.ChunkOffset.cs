namespace Skmr.Editor.Analyzer.Files.Mp4
{
    public partial class Atom
    {
        public class ChunkOffset
        {
            public Byte Version { get; set; }
            public UInt32 Entries { get; set; }
            public UInt32[] ChunkOffsetTable { get; set; }

            public ChunkOffset(byte[] bytes)
            {
                Version = bytes[0];
                Entries = BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));

                ChunkOffsetTable = new UInt32[Entries];
                for (int i = 0; i < Entries; i++) ChunkOffsetTable[i] =
                    BitConverter.ToUInt32(
                        Utility.ReverseRange(bytes[(8 + 4 * i)..(8 + 4 * (i + 1))]));
            }
        }
    }
}
