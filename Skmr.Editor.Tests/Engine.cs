using Skmr.Editor.Engine.Codecs.Apis.Rav1e;
using Y4M = Skmr.Editor.Engine.Y4M;
using H264 = Skmr.Editor.Engine.Bitstreams.H264;
using Y4MB = Skmr.Editor.Engine.Bitstreams.Y4M;
using Skmr.Editor.Engine.Codecs;

namespace Skmr.Editor.Tests
{
    public class Engine
    {
        int width = 480;
        int height = 270;

        [Fact]
        public void TestRav1e()
        {
            string input = "resources\\input.y4m";
            string output = "results\\test1.ivf";

            Y4M.Frame frame;

            using (var s = new H264.StreamReader(input))
            {
                var data = new byte[width * height * 3 / 2];
                s.ReadFrame(out frame);
            }

            if(File.Exists(output)) { File.Delete(output); }

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
            Y4M.Frame frame;

            using (var sr = new Y4MB.StreamReader(input))
            {
                sr.Read(out frame);
            }

            using (var sw = new Y4MB.StreamWriter(output, 480, 270))
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