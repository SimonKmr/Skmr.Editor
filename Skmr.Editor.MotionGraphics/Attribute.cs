using Skmr.Editor.Data.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.MotionGraphics
{
    public class Attribute<T> where T :
            ISubtractionOperators<T, T, Difference<T>>,
            IMultiplyOperators<Difference<T>, float, Difference<T>>,
            IAdditionOperators<T, Difference<T>, T>
    {
        public List<Keyframe<T>> Keyframes { get; set; } = new List<Keyframe<T>>();

        public T Intrapolate(int frame)
        {
            var k = Keyframes;
            int i = k.Count - 1;
            //move index to latest frame
            while (k[i].Frame > frame && i > 0) i--;

            //if is not last frame
            if (i < k.Count - 1)
            {
                //set startpoint of first frame to nil
                var timeIn = frame - k[i].Frame;

                //get time differences between keyframes
                var frameDifference = k[i + 1].Frame - k[i].Frame;

                // if is on keyframe, return value of keyframe
                if (timeIn <= 0) return k[i].Value;

                //get the current timepoint as percent
                var timeInPercent = timeIn / (float)frameDifference;

                //get the difference between the to keyframe values
                Difference<T> d = k[i + 1].Value - k[i].Value;

                //calculate and returns the value of the current frame,
                //based on the Transition function and the difference of the two keyframes
                return k[i].Value + d * k[i].Transition(timeInPercent);
            } 
            else
            {
                //this must be the last frame, so just return the last frames value
                return k[i].Value;
            }
        }
    }
}
