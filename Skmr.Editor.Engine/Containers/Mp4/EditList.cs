namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class EditList : Atom
    {
        public const string Type = "elst";
        //elst
        public Byte Version { get; set; }
        public Int32 EntryCount { get; set; }
        public TableContent[] Table { get; set; }


        public EditList(byte[] bytes)
        {
            Version = bytes[0];
            EntryCount = BitConverter.ToInt32(Utility.ReverseRange(bytes[4..8]));


            List<TableContent> list = new List<TableContent>();
            for (int i = 0; i < EntryCount; i++)
            {
                list.Add(new TableContent(bytes[(i * 12)..((i + 1) * 12)]));
            }

            Table = list.ToArray();
        }


        public class TableContent
        {
            public UInt32 TrackDuration { get; set; }
            public Int32 MediaTime { get; set; }
            public UInt32 MediaRate { get; set; }


            public TableContent(byte[] bytes)
            {
                TrackDuration = BitConverter.ToUInt32(Utility.ReverseRange(bytes[0..4]));
                MediaTime = BitConverter.ToInt32(Utility.ReverseRange(bytes[4..8]));
                MediaRate = BitConverter.ToUInt32(Utility.ReverseRange(bytes[8..12]));
            }
        }
    }
}
