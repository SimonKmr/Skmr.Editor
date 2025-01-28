namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class EditList : Atom
    {
        public const string Type = "elst";
        //elst
        public Byte Version { get; set; }
        public Int32 EntryCount { get; set; }
        public TableContent[] Table { get; set; }


        public EditList(byte[] bytes) : base(bytes)
        {
            base.Type = Type;
            Version = bytes[0];
            EntryCount = bytes.ToInt32(0);


            List<TableContent> list = new List<TableContent>();
            for (int i = 0; i < EntryCount; i++)
            {
                list.Add(new TableContent(bytes[(i * 12 + 16)..((i + 1) * 12 + 16)]));
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
                TrackDuration = bytes.ToUInt32(0, 0);
                MediaTime = bytes.ToInt32(4, 0);
                MediaRate = bytes.ToUInt32(8, 0);
            }
        }
    }
}
