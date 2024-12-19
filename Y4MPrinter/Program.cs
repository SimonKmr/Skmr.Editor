using Skmr.Editor.Engine.Containers.Mp4;
using Skmr.Editor.Engine.Bitstreams.H264;
using Skmr.Editor.Engine.Codecs;
using Skmr.Editor.Engine;

int width = 1920;
int height = 1080;

string output = "test1.ivf";
var outp = File.Open(output, FileMode.Create);

var file = new Mp4File("C:\\Users\\Simon\\Desktop\\test.mp4");
var atoms = file.Read();
var leafs = Mp4File.GetLeafAtoms(atoms);

IVideoDecoder decoder = new OpenH264Dec(width, height);
IVideoEncoder rav1e = new Rav1e(width, height);

var stream = file.GetVideoStreams()[0];
Reader reader = new(stream, width, height);


byte[] bytes;
while(reader.Read(out bytes))
{
    Image<RGB>? frame = null;
    if (!decoder.TryDecode(bytes, out frame))
    {
        continue;
    }

    rav1e.SendFrame(frame);

    var status = rav1e.ReceiveFrame(out byte[]? data);

    if (status == EncoderState.Success && data != null)
    {
        outp.Write(data, 0, data.Length);
    }
}

rav1e.Flush();

while (true)
{
    var status = rav1e.ReceiveFrame(out byte[]? data);
    
    if (status == EncoderState.Ended) break;
    if (status == EncoderState.Success && data != null)
    {
        outp.Write(data, 0, data.Length);
    }
}

while (true) ;