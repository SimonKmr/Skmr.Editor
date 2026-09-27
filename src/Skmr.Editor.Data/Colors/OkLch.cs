using Skmr.Editor.Data.Interfaces;

namespace Skmr.Editor.Data.Colors;

public struct OkLch : IDefault<OkLch>
{
    public OkLch(double l, double c, double h)
    {
        Lightness = l;
        Chroma = c;
        Hue = h;
    }
    public double Lightness;
    public double Chroma;
    public double Hue;
    
    public static OkLch GetDefault()
    {
        return new OkLch(0, 0, 0);
    }
}