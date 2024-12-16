namespace Skmr.Editor.Engine.Containers.Mp4
{
    internal class Utility
    {
        public static byte[] ReverseRange(byte[] bytes)
        {
            byte[] result = new byte[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                result[i] = bytes[bytes.Length - 1 - i];
            }
            return result;
        }
    }
}
