using System.Text;

namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class HandlerReference : Atom
    {
        //hdlr
        public Byte Version { get; set; }
        public UInt32 ComponentType { get; set; }
        public UInt32 ComponentSubtype { get; set; }
        public UInt32 ComponentManufacturer { get; set; }
        public UInt32 ComponentFlags { get; set; }
        public UInt32 ComponentFlagsMask { get; set; }
        public String ComponentName { get; set; }

        public HandlerReference(byte[] bytes) : base(bytes)
        {
            base.Type = "hdlr";
            Version = bytes[0];
            ComponentType = bytes.ToUInt32(0);
            ComponentSubtype = bytes.ToUInt32(4);
            ComponentManufacturer = bytes.ToUInt32(8);
            ComponentFlags = bytes.ToUInt32(12);
            ComponentFlagsMask = bytes.ToUInt32(16);
            ComponentName = Encoding.UTF8.GetString(bytes, 20, bytes.Length - 20);
        }
    }
}
