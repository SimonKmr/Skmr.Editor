using LibAv = Skmr.Editor.Engine.LibAv;
using Skmr.Editor.Engine.Rav1e.Api;
using Rav1e = Skmr.Editor.Engine.Rav1e;
using Y4M = Skmr.Editor.Engine.Y4M;

string path = @"C:\Users\darkf\Desktop\output - Kopie.y4m";
string output = @"test3.ivf";

LibAv.Decoder decoder = new LibAv.Decoder();

return;

var test = new Y4M.File(path);

using (Stream source = File.Open(output, FileMode.Create))
{
    using (var rav1e = new Rav1e.Encoder(1920, 1080, 30))
    {
        int i = 0;

        foreach (var f in rav1e.ReceiveFrame())
        {
            if (i > 300)
            {
                rav1e.Flush();
            }
            else if (f.status == EncoderStatus.NeedMoreData)
            {
                rav1e.SendFrame(test[i]);
                i++;
            }
            else if (f.status == EncoderStatus.Success && f.data != null)
            {
                source.Write(f.data, 0, f.data.Length);
            }
        }
    }
}

while (true) ;