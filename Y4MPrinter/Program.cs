using Y4M = Skmr.Editor.Engine.Y4M;
using Skmr.Editor.Engine.Codecs.Apis.Rav1e;
using Skmr.Editor.Engine.Bitstreams.H264;
using Skmr.Editor.Engine.Codecs;
using H264 = Skmr.Editor.Engine.Bitstreams.H264;

int width = 480;
int height = 270;
Y4M.Frame frame;
TestOpenH264();
TestRav1e();
Console.WriteLine("Done!");
while (true) ;

void TestOpenH264()
{
    string h264Test = @"C:\Users\Simon\Desktop\res.h264";
    int i = 0;
    OpenH264Dec decoder = new OpenH264Dec(width, height);

    using (Stream h264 = File.Open(h264Test, FileMode.Open))
    {
        H264.Reader reader = new H264.Reader(h264);
        while (true)
        {
            var data = reader.ReadFrame();
            if (data.Length == 0) continue;

            if (decoder.TryDecode(data, out frame))
            {
                if(i > 5)
                {
                    break;
                }
                i++;
            }
        }
    }
}

void TestRav1e()
{
    //Setup
    string output = @"test3.ivf";

    //Encoding
    using (Stream source = File.Open(output, FileMode.Create))
    {
        using (var rav1e = new Rav1e(width, height, 30))
        {
            int i = 0;

            while (true)
            {
                var status = rav1e.ReceiveFrame(out byte[]? data);

                if (status == EncoderStatus.LimitReached) break;

                if (i > 300)
                {
                    rav1e.Flush();
                }
                else if (status == EncoderStatus.NeedMoreData)
                {
                    rav1e.SendFrame(frame);
                    i++;
                }
                else if (status == EncoderStatus.Success && data != null)
                {
                    source.Write(data, 0, data.Length);
                }
            }
        }
    }
}