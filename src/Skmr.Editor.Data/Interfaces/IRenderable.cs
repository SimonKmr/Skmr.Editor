namespace Skmr.Editor.Data.Interfaces
{
    public interface IRenderable<T> where T : IColorModel
    {
        Frame<T> GetFrame(int frame);
    }
}
