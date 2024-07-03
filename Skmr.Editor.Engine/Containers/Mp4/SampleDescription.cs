using System.Text;

namespace Skmr.Editor.Engine.Containers.Mp4
{
    public partial class SampleDescription : Atom
    {
        public Byte Version { get; set; }
        public UInt32 Entries { get; set; }
        public Entry[] SampleDescriptionTable { get; set; }


        public SampleDescription(byte[] bytes)
        {
            Version = bytes[0];
            Entries = BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));

            List<Entry> entries = new List<Entry>();
            int index = 8;


            while(index < bytes.Length)
            {
                int end = index + BitConverter.ToInt32(Utility.ReverseRange(bytes[(index + 0)..(index + 4)]));
                entries.Add(new Entry(bytes[index..end]));
                index = end;
            }
            SampleDescriptionTable = entries.ToArray();
        }

        public partial class Entry
        {
            public UInt32 Size { get; set; }
            public String DataFormat { get; set; }
            public UInt16 DataReferenceIndex { get; set; }
            public object MediaData { get; set; }

            public Entry(byte[] bytes)
            {
                Size = BitConverter.ToUInt32(Utility.ReverseRange(bytes[0..4]));
                DataFormat = Encoding.UTF8.GetString(bytes, 4, 4);
                DataReferenceIndex = BitConverter.ToUInt16(Utility.ReverseRange(bytes[14..16]));
                MediaData = AssignMediaData(DataFormat, bytes[16..bytes.Length]);
            }

            private object AssignMediaData(string dataFormat, byte[] bytes)
            {
                if (dataFormat.Equals("avc1")) return new VideoMedia(bytes);
                if (dataFormat.Equals("mp4a")) return new SoundMedia(bytes);
                return null;
            }
        }
    }
}
