using Skmr.Editor.Data.Interfaces;

namespace Skmr.Editor.MotionGraphics.Attributes
{
    public class StaticAttribute<T> : IAttribute<T> where T : IDefault<T>
    {
        public T Value { get; set; }
        public StaticAttribute(T value)
        {
            Value = value;
        }

        public StaticAttribute()
        {
            Value = T.GetDefault();
        }

        public T GetFrame(int frame)
        {
            return Value;
        }
    }
}
