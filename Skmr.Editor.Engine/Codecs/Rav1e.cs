using Skmr.Editor.Data.Colors;
using Skmr.Editor.Engine.Codecs.Apis.Rav1e;
using System.Runtime.InteropServices;

namespace Skmr.Editor.Engine.Codecs
{
    public class Rav1e : IVideoEncoder, IDisposable
    {
        private IntPtr context;
        private IntPtr config;

        public int Width { get; }
        public int Height { get; }
        public int Fps { get; }

        public Rav1e(int width, int height, int fps = 30)
        {
            this.Width = width;
            this.Height = height;
            this.Fps = fps;

            //Create a config file and set width and height
            config = Functions.rav1e_config_default();
            Functions.rav1e_config_parse(config, "width", width.ToString()); //1920
            Functions.rav1e_config_parse(config, "height", height.ToString()); //1080

            //Set Framerate
            var time_base = new Rational() { num = 1, den = Convert.ToUInt64(fps) };
            Functions.rav1e_config_parse(config, "enable_timing_info", "true"); //true
            Functions.rav1e_config_set_time_base(config, time_base);

            //Creates a context out of the config
            context = Functions.rav1e_context_new(config);
        }

        public EncoderState SendFrame(Image<RGB> input)
        {
            var ycbcr = input.ToFrame();
            var status = SendFrame(ycbcr);
            return ToState(status);
        }

        public void Flush()
        {
            Functions.rav1e_send_frame(context, IntPtr.Zero);
        }

        public void Dispose()
        {
            Functions.rav1e_config_unref(config);
            Functions.rav1e_context_unref(context);
        }

        public EncoderStatus SendFrame(Y4M.Frame ycbcr)
        {

            //Creates a frame
            var frame = Functions.rav1e_frame_new(context);
            var y = ycbcr.Get(Y4M.Channel.Y);
            var cb = ycbcr.Get(Y4M.Channel.Cb);
            var cr = ycbcr.Get(Y4M.Channel.Cr);

            //Create references of frame data
            GCHandle arr1 = GCHandle.Alloc(y, GCHandleType.Pinned);
            GCHandle arr2 = GCHandle.Alloc(cb, GCHandleType.Pinned);
            GCHandle arr3 = GCHandle.Alloc(cr, GCHandleType.Pinned);

            //move frame data into encoder frame
            //Stride => count of bytes till next line
            Functions.rav1e_frame_fill_plane(frame, 0, arr1.AddrOfPinnedObject(), new IntPtr(y.Length), new IntPtr(Width), 1);
            Functions.rav1e_frame_fill_plane(frame, 1, arr2.AddrOfPinnedObject(), new IntPtr(cb.Length), new IntPtr(Width / 2), 1);
            Functions.rav1e_frame_fill_plane(frame, 2, arr3.AddrOfPinnedObject(), new IntPtr(cr.Length), new IntPtr(Width / 2), 1);

            //Appends Frame on queue
            var status = Functions.rav1e_send_frame(context, frame);
            Functions.rav1e_frame_unref(frame);
            //Free arrays 
            arr1.Free();
            arr2.Free();
            arr3.Free();

            return status;
        }

        public EncoderState ReceiveFrame(out byte[]? data)
        {
            while (true)
            {
                data = null;

                //Request Packet
                IntPtr ptr = IntPtr.Zero;
                EncoderStatus status;

                //Wait till Frame is encoded
                do
                {
                    status = Functions.rav1e_receive_packet(context, ref ptr);
                } while (status == EncoderStatus.Encoded);

                //Check if Packet is usable
                if (status.Equals(EncoderStatus.LimitReached)) return ToState(status);

                //If Decoding was successful
                if (status == EncoderStatus.Success)
                {
                    //Save Packet to Array
                    var pktRes = Marshal.PtrToStructure<Packet>(ptr);
                    data = new byte[(int)pktRes.len];
                    Marshal.Copy(pktRes.data, data, 0, data.Length);

                    //Allow Packet to be removed from Memory
                    Functions.rav1e_packet_unref(ptr);

                    //Send Frame
                    return ToState(status);
                }
                else
                {
                    return ToState(status);
                }
            }
        }

        private static EncoderState ToState(EncoderStatus status)
        {
            switch (status)
            {
                case EncoderStatus.LimitReached:
                    return EncoderState.Ended;
                case EncoderStatus.Success:
                    return EncoderState.Success;
                default:
                    return EncoderState.Unknown;
            }
        }
    }
}
