namespace Skmr.Editor.MotionGraphics.Elements
{
    internal interface ISubelement<T> : IDisposable
    {
        public T GetValue(int frame);
    }
}
