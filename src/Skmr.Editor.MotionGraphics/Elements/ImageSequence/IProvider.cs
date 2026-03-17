namespace Skmr.Editor.MotionGraphics.Elements.ImageSequence;

public interface IProvider
{
    public byte[] GetFrame(int frame);
}