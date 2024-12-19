using Skmr.Editor.Engine.Codecs.Apis.Rav1e;
using Y4M = Skmr.Editor.Engine.Y4M;
using H264 = Skmr.Editor.Engine.Bitstreams.H264;
using Y4MB = Skmr.Editor.Engine.Bitstreams.Y4M;
using Skmr.Editor.Engine.Codecs;
using Skmr.Editor.Engine;
using Xunit;
using Xunit.Abstractions;

namespace Skmr.Editor.Tests
{
    public class Engine
    {
        private readonly ITestOutputHelper output;

        public Engine(ITestOutputHelper output)
        {
            this.output = output;
        }

        int width = 480;
        int height = 270;

        [Fact]
        public void TestFullRun()
        {
            //packets go missing
            //because OpenH264Dec fails to decode some packages

            

            string input = "resources\\input.h264";
            string output = "results\\test1.ivf";

            if(File.Exists(output)) { File.Delete(output); }

            var inp = File.Open(input, FileMode.Open);
            var outp = File.Open(output, FileMode.CreateNew);

            var reader = new H264.Reader(inp, width, height);

            IVideoDecoder decoder = new OpenH264Dec(width, height);
            IVideoEncoder rav1e = new Rav1e(width, height);

            byte[]? frameData;

            int framesRead = 0;
            int decodingFailed = 0;

            //Read Frames
            while(reader.Read(out frameData))
            {
                Image<RGB>? frame = null;
                framesRead++;

                //Decode Frames
                if (!decoder.TryDecode(frameData, out frame))
                {
                    decodingFailed++;
                    this.output.WriteLine($"Failed in iteration: {framesRead}");
                    continue;
                }
                
                //Reencode Frames
                rav1e.SendFrame(frame);

                //Get Finished Frames
                var status = rav1e.ReceiveFrame(out byte[]? data);

                if (status == EncoderState.Success && data != null)
                {
                    outp.Write(data, 0, data.Length);
                }
            }

            //No more new Frames
            rav1e.Flush();

            //Get Remaining Frames
            while (true)
            {
                var status = rav1e.ReceiveFrame(out byte[]? data);

                if (status == EncoderState.Ended) break;
                if (status == EncoderState.Success && data != null)
                {
                    outp.Write(data, 0, data.Length);
                }
            }

            //Output Stats about en-/decoding
            this.output.WriteLine($"framesRead: {framesRead}");
            this.output.WriteLine($"decodingFailed: {decodingFailed}");
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
    }
}