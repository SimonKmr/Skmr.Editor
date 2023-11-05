using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ReadAtomsRecursive(index, (ulong)bytes.Length, bytes,rootAtoms);
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
            for(int i = 0; i < atoms.Length; i++)
            {
                if (atoms[i].Type == "mvhd") atoms[i].Data = new Atom.MovieHeader(atoms[i].DataRaw);
                if (atoms[i].Type == "tkhd") atoms[i].Data = new Atom.TrackHeader(atoms[i].DataRaw);
                if (atoms[i].Type == "elst") atoms[i].Data = new Atom.EditList(atoms[i].DataRaw);
                if (atoms[i].Type == "clef" || atoms[i].Type == "prof" || atoms[i].Type == "enof") 
                    atoms[i].Data = new Atom.EditList(atoms[i].DataRaw);
                if (atoms[i].Type == "mdhd") atoms[i].Data = new Atom.MediaHeader(atoms[i].DataRaw);
                if (atoms[i].Type == "hdlr") atoms[i].Data = new Atom.HandlerReference(atoms[i].DataRaw);
                if (atoms[i].Type == "smhd") atoms[i].Data = new Atom.SoundMediaInformationHeader(atoms[i].DataRaw);
                if (atoms[i].Type == "dref") atoms[i].Data = new Atom.DataReference(atoms[i].DataRaw);
                if (atoms[i].Type == "ctab") atoms[i].Data = new Atom.ColorTable(atoms[i].DataRaw);
                if (atoms[i].Type == "stts") atoms[i].Data = new Atom.TimeToSample(atoms[i].DataRaw);
                if (atoms[i].Type == "stsc") atoms[i].Data = new Atom.SampleToChunk(atoms[i].DataRaw);
                if (atoms[i].Type == "stsd") atoms[i].Data = new Atom.SampleDescription(atoms[i].DataRaw); //not completely Implemented Missing Media Data Types
                if (atoms[i].Type == "stsz") atoms[i].Data = new Atom.SampleSize(atoms[i].DataRaw);
                if (atoms[i].Type == "stco") atoms[i].Data = new Atom.ChunkOffset(atoms[i].DataRaw);
                if (atoms[i].Type == "vmhd") atoms[i].Data = new Atom.VideoMediaInformationHeader(atoms[i].DataRaw);
                if (atoms[i].Type == "stss") atoms[i].Data = new Atom.SyncSample(atoms[i].DataRaw);
                if (atoms[i].Type == "ctts") atoms[i].Data = new Atom.CompositionOffset(atoms[i].DataRaw);
            }
        }
        public static Atom[] GetLeafAtoms(Atom[] atoms)
        {
            List<Atom> result = new List<Atom>();
            for(int i = 0; i < atoms.Length; i++)
            {
                if(atoms[i].Children == null)
                {
                    result.Add(atoms[i]);
                }
                else
                {
                    var l = GetLeafAtoms(atoms[i].Children);
                    for(int j = 0; j <l.Length; j++)
                    {
                        result.Add(l[j]);
                    }
                }
            }
            return result.ToArray();
        }
        private static void ReadAtomsRecursive(ulong start, ulong end, byte[] bytes, Atom[] atoms)
        {
            for(int i = 0; i < atoms.Length; i++)
            {
                if (!atoms[i].IsContainer)
                {
                    atoms[i].Children = null;
                    continue;
                }
                atoms[i].Children = ReadAtoms(atoms[i].DataStart, atoms[i].DataEnd, bytes);
                
                for(int j = 0; j < atoms[i].Children.Length; j++)
                {
                    if(atoms[i].Children[j].IsContainer)
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
            for(ulong i = 0; i < 4; i++) typeB[i] = original[i + start + 4];
            string type = Encoding.UTF8.GetString(typeB, 0, 4);

            if(length == 1)
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
