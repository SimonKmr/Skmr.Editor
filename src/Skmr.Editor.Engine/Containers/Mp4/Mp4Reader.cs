using System.Text;

namespace Skmr.Editor.Engine.Containers.Mp4
{
    public class Mp4Reader : IVideoContainer
    {
        //https://wiki.multimedia.cx/index.php/QuickTime_container
        //https://developer.apple.com/documentation/quicktime-file-format
        public string Path { get; }
        public Atom[] Atoms { get; }

        public Mp4Reader(string path)
        {
            Path = path;
            Atoms = Read(path);
        }

        public Atom[] Read(string path)
        {
            var bytes = File.ReadAllBytes(path);
            ulong index = 0;
            List<Atom> atoms = new List<Atom>();
            Atom atom = null;
            while (index < (ulong)bytes.Length)
            {
                //think this 
                //var atomLength = bytes.ToUInt32((int)index, 0);
                var data = bytes[(int)index..];

                atom = AtomFactory.CreateAtom(data);
                atoms.Add(atom);
                index += atom.Length;
            }
            
            return atoms.ToArray();
        }

        public Stream[] GetVideoStreams()
        {
            var leafs = GetLeafAtoms(Atoms);
            var mdat = leafs.Where(x => x.Type == "mdat").First();
            return new[] { new MemoryStream(mdat.Data) };
        }

        public void Write()
        {
            throw new NotImplementedException();
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


    }
}
