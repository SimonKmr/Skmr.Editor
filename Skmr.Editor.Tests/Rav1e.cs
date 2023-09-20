using Skmr.Editor.Engine.Codecs.Api;
using Skmr.Editor.Engine.Rav1e;
using Skmr.Editor.Engine.Rav1e.Api;
using Y4M = Skmr.Editor.Engine.Y4M;
using System;

namespace Skmr.Editor.Tests
{
    public class Rav1e
    {
        int width = 480;
        int height = 270;

        [Fact]
        public void Test1()
        {
            string input = "resources\\input.y4m";
            string output = "results\\test1.ivf";

            Y4M.Frame frame;

            using (var s = new Y4M.StreamReader(input))
            {
                var data = new byte[width * height * 3 / 2];
                s.Read(out frame);
            }

            if(File.Exists(output)) { File.Delete(output); }

            using (var s = File.Open(output, FileMode.CreateNew))
            {
                using (var rav1e = new Encoder(width, height))
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
        public void Test2()
        {
            string input = "resources\\input.y4m";
            string output = "results\\test3.y4m";
            Y4M.Frame frame;

            using (var sr = new Y4M.StreamReader(input))
            {
                sr.Read(out frame);
            }

            using (var sw = new Y4M.StreamWriter(output, 480, 270))
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