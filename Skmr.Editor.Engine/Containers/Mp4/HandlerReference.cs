using System.Text;

namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class HandlerReference : Atom
    {
        public const string Type = "hdlr";
        //hdlr
        public Byte Version { get; set; }
        public UInt32 ComponentType { get; set; }
        public UInt32 ComponentSubtype { get; set; }
        public UInt32 ComponentManufacturer { get; set; }
        public UInt32 ComponentFlags { get; set; }
        public UInt32 ComponentFlagsMask { get; set; }
        public String ComponentName { get; set; }
            
        public HandlerReference(byte[] bytes)
        {
            Version = bytes[0];
            ComponentType = BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));
            ComponentSubtype = BitConverter.ToUInt32(Utility.ReverseRange(bytes[8..12]));
            ComponentManufacturer = BitConverter.ToUInt32(Utility.ReverseRange(bytes[12..16]));
            ComponentFlags = BitConverter.ToUInt32(Utility.ReverseRange(bytes[16..20]));
            ComponentFlagsMask = BitConverter.ToUInt32(Utility.ReverseRange(bytes[20..24]));
            ComponentName = Encoding.UTF8.GetString(bytes, 24, bytes.Length-24);
        }
    }
}
