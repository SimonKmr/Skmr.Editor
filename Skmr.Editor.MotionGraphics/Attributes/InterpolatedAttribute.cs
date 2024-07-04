using Skmr.Editor.Data.Colors;
using Skmr.Editor.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics.Attributes
{
    public class InterpolatedAttribute<T> : IAttribute<T> where T :
        ISubtractionOperators<T, T, Difference<T>>,
        IMultiplyOperators<Difference<T>, float, Difference<T>>,
        IAdditionOperators<T, Difference<T>, T>,
        IDefault<T>
    {
        public List<Keyframe<T>> Keyframes { get; set; } = new List<Keyframe<T>>();
        private CurrentFrameInfo info = new CurrentFrameInfo();
        
        public T GetFrame(int frame)
        {
            var k = Keyframes;
            int i = k.Count - 1;

            //if no values was defined, in the keyframes, return the default value
            if (i < 0) return T.GetDefault();

            //move index to latest frame
            while (k[i].Frame > frame && i > 0) i--;

            //if there is only one keyframe or it's the last keyframe
            //skip the method and return the value
            //used for performance improvements
            if (k.Count == 1 ) return Keyframes[0].Value;
            if (k.Count - 1 == i) return Keyframes[k.Count - 1].Value;

            //if it's the same keyframe as previously, then skip the calculations
            if (i != info.CurrentKeyframeIndex)
            {
                info.CurrentKeyframeIndex = i;
                info.CurrentFrameDifference = k[i + 1].Frame - k[i].Frame;
                info.CurrentFrameDifferencePercentage = 1 / (float)info.CurrentFrameDifference;
            }

            //calculates how many frames has passed since the keyframe
            var timeIn = frame - k[i].Frame;

            // if is on keyframe, return value of keyframe
            if (timeIn <= 0) return k[i].Value;

            //get the current timepoint as percent
            var test = info.CurrentFrameDifferencePercentage * timeIn;
            var timeInPercent = timeIn / (float)info.CurrentFrameDifference;

            //get the difference between the to keyframe values
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
    }
}
