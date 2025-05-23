﻿using System.Text;

namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class DataReference : Atom
    {
        public Byte Version { get; set; }
        public UInt32 Entries { get; set; }
        public DataRef[] DataReferences { get; set; }


        public DataReference(byte[] bytes) : base(bytes)
        {
            Type = "dref";
            Version = bytes[0];
            Entries = bytes.ToUInt32(4);

            int index = 16;
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
                Size = bytes.ToUInt32(0,0);
                Type = Encoding.UTF8.GetString(bytes, 4, 4);
                Version = bytes[8];

                if (Type.Equals("alis")) Data = Encoding.UTF8.GetString(bytes, 12, (int)Size - 12);
                if (Type.Equals("rsrc")) Data = Encoding.UTF8.GetString(bytes, 12, (int)Size - 12);
                if (Type.Equals("url ")) Data = Encoding.UTF8.GetString(bytes, 12, (int)Size - 12);
            }
        }
    }
}
