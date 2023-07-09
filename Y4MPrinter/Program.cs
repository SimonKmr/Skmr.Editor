using Skmr.Editor.Engine;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using SE = Skmr.Editor.Engine;
using Skmr.Editor.Engine.Codecs.Rav1e.Api;


string path = @"C:\Users\darkf\Desktop\output - Kopie.y4m";
string output = @"test3.ivf";

var test = new SE.Y4M(path);

SE.Codecs.Rav1e.Encoder rav1e = new SE.Codecs.Rav1e.Encoder(1920, 1080, 60);

int i = 0;
using (Stream source = File.Open(output, FileMode.Create))
{
    foreach (var f in rav1e.ReceiveFrame())
    {
        if (i > 360)
        {
            rav1e.Flush();
        }
        else if (f.status == EncoderStatus.NeedMoreData)
        {
            var y = test.Get(i, Y4M.Channel.Y);
            var cb = test.Get(i, Y4M.Channel.Cb);
            var cr = test.Get(i, Y4M.Channel.Cr);

            rav1e.SendFrame(y, cb, cr);

            i++;
        } 
        else if (f.status == EncoderStatus.Success && f.data != null)
        {
            source.Write(f.data, 0, f.data.Length);
        }

    }
}

return;