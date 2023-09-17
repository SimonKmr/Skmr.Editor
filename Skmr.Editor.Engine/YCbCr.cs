namespace Skmr.Editor.Engine
{
    public struct YCbCr
    {
        public YCbCr(byte y, byte cb, byte cr)
        {
            this.y = y;
            this.cb = cb;
            this.cr = cr;
        }

        public byte y;
        public byte cb;
        public byte cr;
    }
}
