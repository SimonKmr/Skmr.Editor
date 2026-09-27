using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.Engine.Bitstreams.H264;
using Skmr.Editor.Engine.Codecs;
using Skmr.Editor.Engine.Containers.Mp4;

int width = 1920;
int height = 1080;

string output = "test1.ivf";
var outp = File.Open(output, FileMode.Create);

var file = new Mp4Reader("C:\\Users\\Simon\\Desktop\\test.mp4");
var leafs = file.GetLeafAtoms();

IVideoDecoder decoder = new OpenH264Dec(width, height);
IVideoEncoder rav1e = new Rav1e(width, height);

var stream = file.GetVideoStreams()[0];
Reader reader = new(stream, width, height);


byte[] bytes;
while (reader.Read(out bytes!))
{
    Frame<RGB>? frame = null;
    if (!decoder.TryDecode(bytes, out frame))
    {
        continue;
    }

    var status = rav1e.TryEncode(frame!, out byte[]? data);

    if (status == EncoderState.Success && data != null)
    {
        outp.Write(data, 0, data.Length);
    }
}

rav1e.Flush();

while (true)
{
    var status = rav1e.TryEncode(null!, out byte[]? data);

    if (status == EncoderState.Ended) break;
    if (status == EncoderState.Success && data != null)
    {
        outp.Write(data, 0, data.Length);
    }
}

while (true) ;