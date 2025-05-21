using Newtonsoft.Json;
using Skmr.Editor.MotionGraphics.IO;

namespace Skmr.Editor.MotionGraphics
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Keyframe<T>
    {
        [JsonProperty] public T Value { get; set; }
        [JsonProperty] public int Frame { get; set; }
        [JsonProperty] [JsonConverter(typeof(TransitionConverter))] 
        public Func<float, float> Transition { get; set; }
    }
}
