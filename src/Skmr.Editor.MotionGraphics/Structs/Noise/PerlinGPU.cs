using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.Cuda;
using Silk.NET.Vulkan;
using Skmr.Editor.Data;

namespace Skmr.Editor.MotionGraphics.Structs.Noise
{
    /// <summary>
    /// Uses the GPU to create Perlin Noise
    /// </summary>
    public static class PerlinGPU
    {
        //https://github.com/m4rs-mt/ILGPU/issues/728

        public static AMap CreateNoiseMap(int width, int height, double frame, double zoom, double xOffset = 0, double yOffset = 0)
        {
            using Context context = Context.CreateDefault();
            using Accelerator accelerator = context.CreateCudaAccelerator(0);

            using var mapBuffer = accelerator.Allocate2DDenseX<float>(new Index2D(width, height));

            var kernel = accelerator.LoadAutoGroupedStreamKernel<Index2D, double, double, ArrayView2D<float, Stride2D.DenseX>>(NoiseKernel);

            kernel(mapBuffer.Extent.ToIntIndex(), frame, zoom, mapBuffer.View);

            var result = mapBuffer.GetAsArray2D();
            return new AMap(result);
        }

        private static void NoiseKernel(Index2D idx, double z, double zoom, ArrayView2D<float, Stride2D.DenseX> result)
        {
            int[] dPT = {151,160,137,91,90,15,
   131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
   190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
   88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
   77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
   102,143,54,65,25,63,161,1,216,80,73,209,76,132,187,208,89,18,169,200,196,
   135,130,116,188,159,86,164,100,109,198,173,186,3,64,52,217,226,250,124,123,
   5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
   223,183,170,213,119,248,152,2,44,154,163,70,221,153,101,155,167,43,172,9,
   129,22,39,253,19,98,108,110,79,113,224,232,178,185,112,104,218,246,97,228,
   251,34,242,193,238,210,144,12,191,179,162,241,81,51,145,235,249,14,239,107,
   49,192,214,31,181,199,106,157,184,84,204,176,115,121,50,45,127,4,150,254,
   138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180,
    151,160,137,91,90,15,
   131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
   190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
   88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
   77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
   102,143,54,65,25,63,161,1,216,80,73,209,76,132,187,208,89,18,169,200,196,
   135,130,116,188,159,86,164,100,109,198,173,186,3,64,52,217,226,250,124,123,
   5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
   223,183,170,213,119,248,152,2,44,154,163,70,221,153,101,155,167,43,172,9,
   129,22,39,253,19,98,108,110,79,113,224,232,178,185,112,104,218,246,97,228,
   251,34,242,193,238,210,144,12,191,179,162,241,81,51,145,235,249,14,239,107,
   49,192,214,31,181,199,106,157,184,84,204,176,115,121,50,45,127,4,150,254,
   138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180};

            Vec3D[] gradients = {new Vec3D(1,1,0), new Vec3D(-1,1,-0),
            new Vec3D(1,-1,0), new Vec3D(-1,-1,0), new Vec3D(1,0,1),
            new Vec3D(-1,0,1), new Vec3D(1,0,-1), new Vec3D(-1,0,-1),
            new Vec3D(0,1,1), new Vec3D(0,-1,1), new Vec3D(0,1,-1),
            new Vec3D(0,-1,-1)};

            (double x, double y) offset = (0d, 0d);
            double x = (idx.X + offset.x) / zoom;
            double y = (idx.Y + offset.y) / zoom;

            //determine what cube we are in
            int cubeX = ((int)x) & (dPT.Length / 2 - 1);
            int cubeY = ((int)y) & (dPT.Length / 2 - 1);
            int cubeZ = ((int)z) & (dPT.Length / 2 - 1);

            int XIndex = dPT[cubeX] + cubeY;
            int X1Index = dPT[cubeX + 1] + cubeY;
            //indexes for the gradients
            Vec3D V000 = gradients[dPT[dPT[XIndex] + cubeZ] % gradients.Length];
            Vec3D V001 = gradients[dPT[dPT[XIndex] + cubeZ + 1] % gradients.Length];
            Vec3D V010 = gradients[dPT[dPT[XIndex + 1] + cubeZ] % gradients.Length];
            Vec3D V011 = gradients[dPT[dPT[XIndex + 1] + cubeZ + 1] % gradients.Length];
            Vec3D V100 = gradients[dPT[dPT[X1Index] + cubeZ] % gradients.Length];
            Vec3D V101 = gradients[dPT[dPT[X1Index] + cubeZ + 1] % gradients.Length];
            Vec3D V110 = gradients[dPT[dPT[X1Index + 1] + cubeZ] % gradients.Length];
            Vec3D V111 = gradients[dPT[dPT[X1Index + 1] + cubeZ + 1] % gradients.Length];

            //calculate the local x, y and z coordinates (0..1)
            x -= Math.Floor(x);
            y -= Math.Floor(y);
            z -= Math.Floor(z);

            //calculate dot products
            double V000Dot = Common.Dot(V000, x, y, z);
            double V001Dot = Common.Dot(V001, x, y, z - 1);
            double V010Dot = Common.Dot(V010, x, y - 1, z);
            double V011Dot = Common.Dot(V011, x, y - 1, z - 1);
            double V100Dot = Common.Dot(V100, x - 1, y, z);
            double V101Dot = Common.Dot(V101, x - 1, y, z - 1);
            double V110Dot = Common.Dot(V110, x - 1, y - 1, z);
            double V111Dot = Common.Dot(V111, x - 1, y - 1, z - 1);

            //calculate smoothed x, y and z values. These are used to get
            //a smoother interpolation between the dot products of the 
            //gradients and local coords
            double smoothedX = Common.SmoothToSCurve(x);
            double smoothedY = Common.SmoothToSCurve(y);
            double smoothedZ = Common.SmoothToSCurve(z);

            ////linearly interpolate the dot products
            double V000V100Val = Common.LinearlyInterpolate(V000Dot, V100Dot, smoothedX);
            double V001V101Val = Common.LinearlyInterpolate(V001Dot, V101Dot, smoothedX);
            double V010V110Val = Common.LinearlyInterpolate(V010Dot, V110Dot, smoothedX);
            double V011V111Val = Common.LinearlyInterpolate(V011Dot, V111Dot, smoothedX);

            double ZZeroPlaneVal = Common.LinearlyInterpolate(V000V100Val, V010V110Val, smoothedY);
            double ZOnePlaneVal = Common.LinearlyInterpolate(V001V101Val, V011V111Val, smoothedY);

            result[idx.X, idx.Y] = (float)Common.LinearlyInterpolate(ZZeroPlaneVal, ZOnePlaneVal, smoothedZ);
        }




    }
}
