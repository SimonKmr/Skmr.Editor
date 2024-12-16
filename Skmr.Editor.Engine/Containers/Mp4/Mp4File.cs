using System.Text;

namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class Mp4File : IVideoContainer
    {
        //https://wiki.multimedia.cx/index.php/QuickTime_container
        //https://developer.apple.com/documentation/quicktime-file-format
        public string Path { get; set; }
        public Atom[] Atoms { get; }

        public Mp4File(string path)
        {
            Path = path;
            Atoms = Read();
        }

        public Atom[] Read()
        {
            var bytes = File.ReadAllBytes(Path);
            ulong index = 0;

            var rootAtoms = ReadAtoms(index, (ulong)bytes.Length, bytes);
            ReadAtomsRecursive(index, (ulong)bytes.Length, bytes, rootAtoms);
            var leafs = GetLeafAtoms(rootAtoms);
            AssignLeafsAtomClasses(leafs);
            return rootAtoms;
        }

        public Stream[] GetVideoStreams()
        {
            var leafs = GetLeafAtoms(Atoms);
            var mdat = leafs.Where(x => x.Type == "mdat").First();
            return new[] { new MemoryStream(mdat.DataRaw) };
        }

        public void Write()
        {
            throw new NotImplementedException();
        }

        private void AssignLeafsAtomClasses(Atom[] atoms)
        {
            for (int i = 0; i < atoms.Length; i++)
            {
                if (atoms[i].Type == "mvhd") atoms[i] = new MovieHeader(atoms[i].DataRaw);
                if (atoms[i].Type == "tkhd") atoms[i] = new TrackHeader(atoms[i].DataRaw);
                if (atoms[i].Type == "elst") atoms[i] = new EditList(atoms[i].DataRaw);
                if (atoms[i].Type == "clef" || atoms[i].Type == "prof" || atoms[i].Type == "enof")
                    atoms[i] = new EditList(atoms[i].DataRaw);
                if (atoms[i].Type == "mdhd") atoms[i] = new MediaHeader(atoms[i].DataRaw);
                if (atoms[i].Type == "hdlr") atoms[i] = new HandlerReference(atoms[i].DataRaw);
                if (atoms[i].Type == "smhd") atoms[i] = new SoundMediaInformationHeader(atoms[i].DataRaw);
                if (atoms[i].Type == "dref") atoms[i] = new DataReference(atoms[i].DataRaw);
                if (atoms[i].Type == "ctab") atoms[i] = new ColorTable(atoms[i].DataRaw);
                if (atoms[i].Type == "stts") atoms[i] = new TimeToSample(atoms[i].DataRaw);
                if (atoms[i].Type == "stsc") atoms[i] = new SampleToChunk(atoms[i].DataRaw);
                if (atoms[i].Type == "stsd") atoms[i] = new SampleDescription(atoms[i].DataRaw); //not completely Implemented Missing Media Data Types
                if (atoms[i].Type == "stsz") atoms[i] = new SampleSize(atoms[i].DataRaw);
                if (atoms[i].Type == "stco") atoms[i] = new ChunkOffset(atoms[i].DataRaw);
                if (atoms[i].Type == "vmhd") atoms[i] = new VideoMediaInformationHeader(atoms[i].DataRaw);
                if (atoms[i].Type == "stss") atoms[i] = new SyncSample(atoms[i].DataRaw);
                if (atoms[i].Type == "ctts") atoms[i] = new CompositionOffset(atoms[i].DataRaw);
            }
        }
        public static Atom[] GetLeafAtoms(Atom[] atoms)
        {
            List<Atom> result = new List<Atom>();
            for (int i = 0; i < atoms.Length; i++)
            {
                if (atoms[i].Children == null)
                {
                    result.Add(atoms[i]);
                }
                else
                {
                    var l = GetLeafAtoms(atoms[i].Children);
                    for (int j = 0; j < l.Length; j++)
                    {
                        result.Add(l[j]);
                    }
                }
            }
            return result.ToArray();
        }
        private static void ReadAtomsRecursive(ulong start, ulong end, byte[] bytes, Atom[] atoms)
        {
            for (int i = 0; i < atoms.Length; i++)
            {
                if (!atoms[i].IsContainer)
                {
                    atoms[i].Children = null;
                    continue;
                }
                atoms[i].Children = ReadAtoms(atoms[i].DataStart, atoms[i].DataEnd, bytes);

                for (int j = 0; j < atoms[i].Children.Length; j++)
                {
                    if (atoms[i].Children[j].IsContainer)
                        ReadAtomsRecursive(atoms[i].Children[j].DataStart, atoms[i].Children[j].DataEnd, bytes, atoms[i].Children);
                }
            }
        }
        private static Atom[] ReadAtoms(ulong start, ulong end, byte[] bytes)
        {
            List<Atom> atoms = new List<Atom>();
            ulong index = start;
            while (index < end)
            {
                var atom = ReadAtomHead(index, bytes);
                atoms.Add(atom);
                index += atom.Length;
            }
            return atoms.ToArray();
        }
        private static Atom ReadAtomHead(ulong start, byte[] original)
        {
            byte[] size = new byte[8];
            for (ulong i = 0; i < 4; i++) size[i + 4] = original[i + start];
            Array.Reverse(size);
            ulong length = BitConverter.ToUInt64(size);

            byte[] typeB = new byte[4];
            for (ulong i = 0; i < 4; i++) typeB[i] = original[i + start + 4];
            string type = Encoding.UTF8.GetString(typeB, 0, 4);

            if (length == 1)
            {
                size = new byte[8];
                for (ulong i = 0; i < 8; i++) size[i] = original[i + start + 8];
                Array.Reverse(size);
                length = BitConverter.ToUInt64(size);
            }

            return new Atom(start, length, type, original);
        }


    }
}
