namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class Atom
    {
        public ulong Index { get; set; }
        public ulong Length { get; set; }
        public string Type { get; set; }


        public bool IsContainer { get => IsContainerAtom(Type); }
        public ulong DataStart { get => Index + 8; }
        public ulong DataEnd { get => Index + Length; }
        public byte[] DataRaw { get; set; }

        public Atom() { }
        public Atom(ulong index, ulong length, string type, byte[] bytes)
        {
            Index = index;
            Length = length;
            Type = type;
            DataRaw = bytes[(int)DataStart..(int)DataEnd];
        }

        public Atom[] Children { get; set; }

        private static bool IsContainerAtom(string type)
        {
            switch (type)
            {
                case "moov": return true;
                case "clip": return true;
                case "udta": return true;
                case "trak": return true;
                case "matt": return true;
                case "edts": return true;
                case "mdia": return true;
                case "minf": return true;
                case "dinf": return true;
                case "stbl": return true;
            }
            return false;
        }
    }
}
