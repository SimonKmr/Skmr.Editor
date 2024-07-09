namespace Skmr.Editor.MotionGraphics.Attributes
{
    public class ProcedualAttribute<T> : IAttribute<T>
    {
        public Func<float, T> Generator { get; set; }
        public T GetFrame(int frame)
        {
            return Generator(frame);
        }
    }
}
