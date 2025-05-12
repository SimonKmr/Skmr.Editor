using System.Text;

namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class Atom
    {
        public ulong Index { get; private set; }
        public ulong Length { get; private set; }
        public string Type { get; internal set; }

        internal int base_offset = 8;

        public bool IsContainer { get => IsContainerAtom(Type); }
        public byte[] Data { get; set; }

        public Atom(byte[] bytes)
        {
            Length = GetAtomLength(bytes);
            Type = GetAtomType(bytes);
            Data = bytes[0..(int)Length];
            if (!IsContainer) return;

            ulong index = 0;
            List<Atom> children = new List<Atom>();
            while (index < Length - 8)
            {
                var b = bytes[(int)(index + 8)..(int)(Length)];
                if (b.Length == 0) break;
                
                var atom = AtomFactory.CreateAtom(b);
                children.Add(atom);
                index += atom.Length;
            }

            Children = children.ToArray();
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

        internal static ulong GetAtomLength(byte[] data)
        {
            byte[] size = new byte[8];
            for (ulong i = 0; i < 4; i++) size[i + 4] = data[i];
            Array.Reverse(size);
            var len = BitConverter.ToUInt64(size);

            if (len == 1)
            {
                size = new byte[8];
                for (ulong i = 0; i < 8; i++) size[i] = data[i + 8];
                Array.Reverse(size);
                len = BitConverter.ToUInt64(size);
            }

            return len;
        }

        internal static string GetAtomType(byte[] data)
        {
            byte[] typeB = new byte[4];
            for (ulong i = 0; i < 4; i++) typeB[i] = data[i + 4];
            return Encoding.UTF8.GetString(typeB, 0, 4);
        }
    }
}
