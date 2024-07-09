namespace Skmr.Editor.MotionGraphics
{
    public class Function
    {
        public static float Logistic(float x)
        {
            var k = 9f;
            var t = Math.Pow(float.E, -k * (x - 0.5));
            return 1 / (1 + (float)t);
        }
        public static float Linear(float x) => x;
        public static float Square(float x) => 1 - (x - 1) * (x - 1);
        public static float Cubic(float x) => (x - 1) * (x - 1) * (x - 1) + 1;
    }
}
