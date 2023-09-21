using Skmr.Editor.Engine.Codecs.Apis.Rav1e;
using Y4M = Skmr.Editor.Engine.Y4M;
using H264 = Skmr.Editor.Engine.Bitstreams.H264;
using Y4MB = Skmr.Editor.Engine.Bitstreams.Y4M;
using Skmr.Editor.Engine.Codecs;
using Skmr.Editor.Engine;

namespace Skmr.Editor.Tests
{
    public class Engine
    {
        int width = 480;
        int height = 270;

        [Fact]
        public void TestRav1e()
        {
            Image<RGB>? frame = null;

            string input = "resources\\input.h264";
            string output = "results\\test1.ivf";

            if(File.Exists(output)) { File.Delete(output); }

            var inp = File.Open(input, FileMode.Open);
            var reader = new H264.Reader(inp, width, height);
            var decoder = new OpenH264Dec(width, height);

            byte[]? frameData;

            while(true)
            {
                var data = new byte[width * height * 3 / 2];
                reader.Read(out frameData);

                if(decoder.TryDecode(frameData, out frame))
                {
                    break;
                }
            }

            using (var s = File.Open(output, FileMode.CreateNew))
            {
                using (var rav1e = new Rav1e(width, height))
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
                            s.Write(data, 0, data.Length);
                        }
                    }
                }
            }
        }

        [Fact]
        public void TestY4MReaderAndWriter()
        {
            string input = "resources\\input.y4m";
            string output = "results\\test3.y4m";
            
            var i = File.Open(input, FileMode.Open);
            var o = File.Open(output, FileMode.Create);
            
            Image<RGB> frame;
            
            using (var sr = new Y4MB.Reader(i, width, height))
            {
                sr.Read(out frame);
            }

            using (var sw = new Y4MB.Writer(o, width, height))
            {
                sw.Write(frame);
            }
        }

        [Fact]
        public void Test3()
        {

        }
    }
}