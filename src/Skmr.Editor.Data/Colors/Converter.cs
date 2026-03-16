using System.Drawing;

namespace Skmr.Editor.Data.Colors;

public static class Converter
{
    public static OkLch ToOkLch(this RGB color)
    {
        // Normalize RGB
        double sr = color.r / 255.0;
        double sg = color.g / 255.0;
        double sb = color.b / 255.0;

        // sRGB → Linear
        double lr = SrgbToLinear(sr);
        double lg = SrgbToLinear(sg);
        double lb = SrgbToLinear(sb);

        // Linear RGB → LMS
        double l = 0.4122214708 * lr + 0.5363325363 * lg + 0.0514459929 * lb;
        double m = 0.2119034982 * lr + 0.6806995451 * lg + 0.1073969566 * lb;
        double s = 0.0883024619 * lr + 0.2817188376 * lg + 0.6299787005 * lb;

        // Nonlinear transform
        double l_ = Math.Cbrt(l);
        double m_ = Math.Cbrt(m);
        double s_ = Math.Cbrt(s);

        // LMS → OkLab
        double L = 0.2104542553 * l_ + 0.7936177850 * m_ - 0.0040720468 * s_;
        double A = 1.9779984951 * l_ - 2.4285922050 * m_ + 0.4505937099 * s_;
        double B = 0.0259040371 * l_ + 0.7827717662 * m_ - 0.8086757660 * s_;

        // OkLab → OKLCH
        double C = Math.Sqrt(A * A + B * B);
        double H = Math.Atan2(B, A) * 180.0 / Math.PI;

        if (H < 0)
            H += 360;

        return new OkLch(L, C, H);
    }
    
    public static double SrgbToLinear(double c)
    {
        if (c <= 0.04045)
            return c / 12.92;
        else
            return Math.Pow((c + 0.055) / 1.055, 2.4);
    }
    
    private static double LinearToSrgb(double c)
    {
        if (c <= 0.0031308)
            return 12.92 * c;
        else
            return 1.055 * Math.Pow(c, 1.0 / 2.4) - 0.055;
    }
    
    private static byte ClampToByte(double v)
    {
        return (byte)Math.Round(Math.Clamp(v, 0, 255));
    }
    
    public static RGB ToRgb(this OkLch color)
    {
        // Convert hue to radians
        double hRad = color.Hue * Math.PI / 180.0;

        // OKLCH → OkLab
        double A = color.Chroma * Math.Cos(hRad);
        double B = color.Chroma * Math.Sin(hRad);

        // OkLab → LMS'
        double l_ = color.Lightness + 0.3963377774 * A + 0.2158037573 * B;
        double m_ = color.Lightness - 0.1055613458 * A - 0.0638541728 * B;
        double s_ = color.Lightness - 0.0894841775 * A - 1.2914855480 * B;

        // Cube
        double l = l_ * l_ * l_;
        double m = m_ * m_ * m_;
        double s = s_ * s_ * s_;

        // LMS → linear RGB
        double r = +4.0767416621 * l - 3.3077115913 * m + 0.2309699292 * s;
        double g = -1.2684380046 * l + 2.6097574011 * m - 0.3413193965 * s;
        double b = -0.0041960863 * l - 0.7034186147 * m + 1.7076147010 * s;

        // Linear → sRGB
        r = LinearToSrgb(r);
        g = LinearToSrgb(g);
        b = LinearToSrgb(b);

        // Convert to 0–255
        byte R = ClampToByte(r * 255.0);
        byte G = ClampToByte(g * 255.0);
        byte Bc = ClampToByte(b * 255.0);

        return new RGB(R, G, Bc);
    }
    
    public static RGB ToRgb(this YCbCr color)
    {
        double Y = color.y;
        double Cb = color.cb;
        double Cr = color.cr;

        int r = (int)(Y + 1.40200 * (Cr - 0x80));
        int g = (int)(Y - 0.34414 * (Cb - 0x80) - 0.71414 * (Cr - 0x80));
        int b = (int)(Y + 1.77200 * (Cb - 0x80));

        return new RGB(
            (byte)Math.Max(0, Math.Min(255, r)),
            (byte)Math.Max(0, Math.Min(255, g)),
            (byte)Math.Max(0, Math.Min(255, b))
        );
    }
    
    public static RGB ToRgb(this YUV color)
    {
        // Conversion formula
        double yd = color.y;
        double ud = color.u - 128;
        double vd = color.v - 128;

        var red = (int)Math.Round(yd + 1.13983 * vd);
        var green = (int)Math.Round(yd - 0.39465 * ud - 0.58060 * vd);
        var blue = (int)Math.Round(yd + 2.03211 * ud);

        // Clamp the RGB values to the valid 8-bit range (0-255)
        red = Math.Max(0, Math.Min(255, red));
        green = Math.Max(0, Math.Min(255, green));
        blue = Math.Max(0, Math.Min(255, blue));

        return new RGB
        {
            r = (byte)red,
            g = (byte)green,
            b = (byte)blue,
        };
    }
    
    public static YUV ToYuv(this RGB color)
    {
        // Conversion formula
        double rd = color.r / 255.0;
        double gd = color.g / 255.0;
        double bd = color.b / 255.0;

        var y = (int)Math.Round(0.299 * rd + 0.587 * gd + 0.114 * bd);
        var u = (int)Math.Round(-0.14713 * rd - 0.28886 * gd + 0.436 * bd) + 128;
        var v = (int)Math.Round(0.615 * rd - 0.51498 * gd - 0.10001 * bd) + 128;

        // Clamp the YUV values to the valid 8-bit range
        y = Math.Max(0, Math.Min(255, y));
        u = Math.Max(0, Math.Min(255, u));
        v = Math.Max(0, Math.Min(255, v));

        return new YUV
        {
            y = (byte)y,
            u = (byte)u,
            v = (byte)v,
        };
    }
    public static YUV ToYuv(this YCbCr color)
    {
        double yd = color.y;
        double crd = color.cr - 128;
        double cbd = color.cb - 128;

        var yuvY = (int)Math.Round(yd + 1.402 * crd);
        var yuvU = (int)Math.Round(yd - 0.34414 * cbd - 0.71414 * crd);
        var yuvV = (int)Math.Round(yd + 1.772 * cbd);

        // Clamp the YUV values to the valid 8-bit range
        yuvY = Math.Max(0, Math.Min(255, yuvY));
        yuvU = Math.Max(0, Math.Min(255, yuvU));
        yuvV = Math.Max(0, Math.Min(255, yuvV));

        return new YUV
        {
            y = (byte)yuvY,
            u = (byte)yuvU,
            v = (byte)yuvV,
        };
    }
    
    public static YCbCr ToYCbCr(this RGB color)
    {
        double R = (double)color.r / 255;
        double G = (double)color.g / 255;
        double B = (double)color.b / 255;

        double Y = 0.299 * R + 0.587 * G + 0.114 * B;
        double Cb = -0.169 * R - 0.331 * G + 0.500 * B;
        double Cr = 0.500 * R - 0.419 * G - 0.081 * B;

        return new YCbCr(
            (byte)(Y * 255),
            (byte)((Cb + 0.5) * 255),
            (byte)((Cr + 0.5) * 255)
        );
    }
    
    public static YCbCr ToYCbCr(this YUV color)
    {
        return new YCbCr
        {
            y = color.y,
            cr = (byte)(color.v - 128),
            cb = (byte)(color.u - 128),
        };
    }
}