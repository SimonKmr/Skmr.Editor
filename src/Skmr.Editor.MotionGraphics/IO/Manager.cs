using Newtonsoft.Json;
using Skmr.Editor.MotionGraphics.Sequences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.IO
{
    public static class Manager
    {
        public static string ToJson(this Sequence seq)
        {
            return JsonConvert.SerializeObject(seq, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
            });
        }

        public static Sequence FromJson(string json)
        {
            var res = JsonConvert.DeserializeObject<Sequence>(json, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
            });
            if (res != null)
                return res;

            throw new Exception();
        }
    }
}
