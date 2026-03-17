using System.Collections;
using Newtonsoft.Json;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.Data.Interfaces;
using System.Numerics;

namespace Skmr.Editor.MotionGraphics.Attributes
{
    [JsonObject(MemberSerialization.OptIn)]
    public class InterpolatedAttribute<T> : IAttribute<T>, IList<Keyframe<T>> where T :
        ISubtractionOperators<T, T, Difference<T>>,
        IMultiplyOperators<Difference<T>, float, Difference<T>>,
        IAdditionOperators<T, Difference<T>, T>,
        IDefault<T>
    {
        [JsonProperty] 
        private List<Keyframe<T>> _keyframes = new ();
        private CurrentFrameInfo _info = new ();

        public T GetFrame(int frame)
        {
            var k = _keyframes;
            int i = k.Count - 1;

            //if no values was defined, in the keyframes, return the default value
            if (i < 0) return T.GetDefault();

            //move index to latest frame
            while (k[i].Frame > frame && i > 0) i--;

            //if there is only one keyframe, or it's the last keyframe
            //skip the method and return the value
            //used for performance improvements
            if (k.Count == 1) return k[0].Value;
            if (k.Count - 1 == i) return k[k.Count - 1].Value;

            //if it's the same keyframe as previously, then skip the calculations
            if (i != _info.CurrentKeyframeIndex)
            {
                _info.CurrentKeyframeIndex = i;
                _info.CurrentFrameDifference = k[i + 1].Frame - k[i].Frame;
                _info.CurrentFrameDifferencePercentage = 1 / (float)_info.CurrentFrameDifference;
            }

            //calculates how many frames has passed since the keyframe
            var timeIn = frame - k[i].Frame;

            // if is on keyframe, return value of keyframe
            if (timeIn <= 0) return k[i].Value;

            //get the current timepoint as percent
            var test = _info.CurrentFrameDifferencePercentage * timeIn;
            var timeInPercent = timeIn / (float)_info.CurrentFrameDifference;

            //get the difference between the two keyframe values
            Difference<T> d = k[i + 1].Value - k[i].Value;

            //calculate and returns the value of the current frame,
            //based on the Transition function and the difference of the two keyframes
            return k[i].Value + d * k[i].Transition(timeInPercent);
        }

        private class CurrentFrameInfo
        {
            public int CurrentKeyframeIndex { get; set; } = -1;
            public int CurrentFrameDifference { get; set; }
            public float CurrentFrameDifferencePercentage { get; set; }
        }

        public IEnumerator<Keyframe<T>> GetEnumerator()
            => _keyframes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public void Add(Keyframe<T> item)
            => _keyframes.Add(item);

        public void Clear()
            => _keyframes.Clear();

        public bool Contains(Keyframe<T> item)
            => _keyframes.Contains(item);

        public void CopyTo(Keyframe<T>[] array, int arrayIndex)
            => _keyframes.CopyTo(array, arrayIndex);

        public bool Remove(Keyframe<T> item)
            => _keyframes.Remove(item);

        public int Count => _keyframes.Count; 
        public bool IsReadOnly =>  false; 
        public int IndexOf(Keyframe<T> item)
            => _keyframes.IndexOf(item);

        public void Insert(int index, Keyframe<T> item)
            => _keyframes.Insert(index, item);

        public void RemoveAt(int index)
            => _keyframes.RemoveAt(index);

        public Keyframe<T> this[int index]
        {
            get => _keyframes[index];
            set => _keyframes[index] = value;
        }
    }
}
