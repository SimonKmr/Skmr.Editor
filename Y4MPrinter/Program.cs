using Y4M = Skmr.Editor.Engine.Y4M;
using Skmr.Editor.Engine.Codecs.Apis.Rav1e;
using Skmr.Editor.Engine.Bitstreams.H264;
using Skmr.Editor.Engine.Codecs;
using H264 = Skmr.Editor.Engine.Bitstreams.H264;
using OpenH264Sample;
using System.Drawing;
using Skmr.Editor.Engine.Y4M;
using Skmr.Editor.Engine;
using System;

int width = 480;
int height = 270;
Image<RGB>? frame;
Console.WriteLine("Decoding");
H264DecodeAvi(@"C:\Users\Simon\Desktop\res.h264",480,270);
Console.WriteLine("Done!");
//TestOpenH264();
//TestRav1e();

while (true) ;

unsafe void H264DecodeAvi(string path, int width, int height)
{
    
    // create decoder
    //var decoder = new OpenH264Lib.Decoder(OpenH264Dec.dllPath);

    // open file
    var h264 = File.OpenRead(path);
    var reader = new H264.Reader(h264,width,height);

    var decoder = new OpenH264Dec(width,height);
    byte[] frameData;

    int countFramesRead = 0;
    int countFramesDecoded = 0;

    while (true)
    {
        if(reader.Read(out frameData))
        {
            countFramesRead++;
        }
        else { break; }
        
        if(decoder.TryDecode(frameData, out frame))
        {
            countFramesDecoded++;
            var bitmap = new Bitmap(width, height);
            
            for(int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var color = frame.Get(x, y);
                    bitmap.SetPixel(x, y, Color.FromArgb(color.r, color.g, color.b));
                }
            }
            bitmap.Save($"imgs\\res_{countFramesDecoded}.png");
            bitmap.Dispose();
        }

        Console.WriteLine($"Read: {countFramesRead} Decoded: {countFramesDecoded}");
    }
}