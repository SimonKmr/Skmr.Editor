using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Engine.Containers.Mp4
{
    public static class AtomFactory
    {
        public static Atom CreateAtom(byte[] data)
        {
            var type = Atom.GetAtomType(data);
            switch (type)
            {
                case "mvhd": return new MovieHeader(data);
                case "tkhd": return new TrackHeader(data);
                case "elst": return new EditList(data);
                case "clef": return new EditList(data);
                case "prof": return new EditList(data);
                case "enof": return new EditList(data);
                case "mdhd": return new MediaHeader(data);
                case "hdlr": return new HandlerReference(data);
                case "smhd": return new SoundMediaInformationHeader(data);
                case "dref": return new DataReference(data);
                case "ctab": return new ColorTable(data);
                case "stts": return new TimeToSample(data);
                case "stsc": return new SampleToChunk(data);
                case "stsd": return new SampleDescription(data); //not completely Implemented Missing Media Data Types
                case "stsz": return new SampleSize(data);
                case "stco": return new ChunkOffset(data);
                case "vmhd": return new VideoMediaInformationHeader(data);
                case "stss": return new SyncSample(data);
                case "ctts": return new CompositionOffset(data);
                default: return new Atom(data);
            }                 
        }




    }
}
