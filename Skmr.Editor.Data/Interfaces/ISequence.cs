namespace Skmr.Editor.Data.Interfaces
{
    public interface ISequence
    {
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }

        public void Render();
    }
}
