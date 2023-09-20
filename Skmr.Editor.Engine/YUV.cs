namespace Skmr.Editor.Engine
{
    public struct YUV
    {
        public YUV(byte y, byte u, byte v)
        {
            this.y = y;
            this.u = u;
            this.v = v;
        }

        public byte y;
        public byte u;
        public byte v;
    }
}
