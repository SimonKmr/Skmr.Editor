namespace Skmr.Editor.MotionGraphics.Elements;

public interface IProvider
{
    public byte[] GetPath(int frame);
}