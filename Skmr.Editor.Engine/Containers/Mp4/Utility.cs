namespace Skmr.Editor.Engine.Containers.Mp4
{
    internal static class Utility
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

        public static uint ToUInt32(this byte[] bytes, int start, int offset = 12)
        {
            byte[] b = bytes[(offset + start)..(offset + start + 4)];
            byte[] r = ReverseRange(b);
            uint result = BitConverter.ToUInt32(r);
            return result;
        }

        public static int ToInt32(this byte[] bytes, int start, int offset = 12)
        {
            byte[] b = bytes[(offset + start)..(offset + start + 4)];
            byte[] r = ReverseRange(b);
            int result = BitConverter.ToInt32(r);
            return result;
        }

        public static UInt16 ToUInt16(this byte[] bytes, int start, int offset = 12)
        {
            byte[] b = bytes[(offset + start)..(offset + start + 2)];
            byte[] r = ReverseRange(b);
            UInt16 result = BitConverter.ToUInt16(r);
            return result;
        }
    }
}
