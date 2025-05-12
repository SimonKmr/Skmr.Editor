using Skmr.Editor.Engine.Containers.Mp4;

namespace Mp4ReaderTestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mp4Reader reader = new Mp4Reader("C:\\Users\\darkf\\Documents\\New track 2.mp4");
            Console.WriteLine(reader.Atoms); 
        }
    }
}
