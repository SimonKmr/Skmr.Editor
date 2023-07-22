namespace Skmr.Editor.Engine
{
    public interface IIndexable<T>
    {
        T this[int index] { get; set; }
    }
}
