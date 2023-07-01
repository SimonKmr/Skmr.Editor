using System.Text;

namespace Skmr.Editor.Engine.Containers.Mp4
{
    public partial class Atom
    {
        public class DataReference
        {
            public const string Type = "dref";
            public Byte Version { get; set; }
            public UInt32 Entries { get; set; }
            public DataRef[] DataReferences { get; set; }


            public DataReference(byte[] bytes)
            {
                Version = bytes[0];
                Entries = BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));

                int index = 8;
                List<DataRef> dr = new List<DataRef>();
                do
                {
                    //maybe do different, give whole range and then let itself pick the right bytes
                    int end = index + BitConverter.ToInt32(Utility.ReverseRange(bytes[index..(index + 4)]));
                    dr.Add(new DataRef(bytes[index..end]));
                    index = end;
                } 
                while (index < bytes.Length);
                DataReferences = dr.ToArray();
            }

            public class DataRef
            {
                public UInt32 Size { get; set; }
                public String Type { get; set; }
                public Byte Version { get; set; }
                object Data { get; set; }

                public DataRef(byte[] bytes)
                {
                    Size = BitConverter.ToUInt32(Utility.ReverseRange(bytes[0..4]));
                    Type = Encoding.UTF8.GetString(bytes, 4, 4);
                    Version = bytes[8];

                    if (Type.Equals("alis")) Data = Encoding.UTF8.GetString(bytes, 12, (int)Size - 12);
                    if (Type.Equals("rsrc")) Data = Encoding.UTF8.GetString(bytes, 12, (int)Size - 12);
                    if (Type.Equals("url ")) Data = Encoding.UTF8.GetString(bytes, 12, (int)Size - 12);
                }
            }
        }
    }
}
