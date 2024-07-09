namespace Skmr.Editor.MotionGraphics.Attributes
{
    public interface IAttribute<T>
    {
        public abstract T GetFrame(int frame);
    }
}
