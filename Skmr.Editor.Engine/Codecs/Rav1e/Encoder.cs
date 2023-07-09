using Skmr.Editor.Engine.Codecs.Rav1e.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Editor.Engine.Codecs.Rav1e
{
    public class Encoder : IDisposable
    {
        private IntPtr context;
        private IntPtr config;

        private int width;
        private int height;
        private int fps;


        public Encoder(int width, int height, int fps = 30)
        {
            this.width = width;
            this.height = height;
            this.fps = fps;
            
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

        public EncoderStatus SendFrame(byte[] y, byte[] cb, byte[] cr)
        {
            //Creates a frame
            var frame = Functions.rav1e_frame_new(context);

            //Create references of frame data
            GCHandle arr1 = GCHandle.Alloc(y, GCHandleType.Pinned);
            GCHandle arr2 = GCHandle.Alloc(cb, GCHandleType.Pinned);
            GCHandle arr3 = GCHandle.Alloc(cr, GCHandleType.Pinned);

            //move frame data into encoder frame
            //Stride => count of bytes till next line
            Functions.rav1e_frame_fill_plane(frame, 0, arr1.AddrOfPinnedObject(), new IntPtr(y.Length), new IntPtr(width), 1);
            Functions.rav1e_frame_fill_plane(frame, 1, arr2.AddrOfPinnedObject(), new IntPtr(cb.Length), new IntPtr(width / 2), 1);
            Functions.rav1e_frame_fill_plane(frame, 2, arr3.AddrOfPinnedObject(), new IntPtr(cr.Length), new IntPtr(width / 2), 1);

            //Appends Frame on queue
            var status = Functions.rav1e_send_frame(context, frame);

            //Free arrays for allocated planes
            arr1.Free();
            arr2.Free();
            arr3.Free();

            return status;
        }

        public IEnumerable<(EncoderStatus status, byte[]? data)> ReceiveFrame()
        {
            while (true)
            {
                //Request Packet
                IntPtr ptr = IntPtr.Zero;
                var status = Functions.rav1e_receive_packet(context, ref ptr);

                //Check if Packet is usable
                if (status.Equals(EncoderStatus.LimitReached)) break;
                
                //If Decoding was successful
                if (status == EncoderStatus.Success)
                {
                    //Save Packet to Array
                    var pktRes = Marshal.PtrToStructure<Packet>(ptr);
                    var bytes = new byte[(int)pktRes.len];
                    Marshal.Copy(pktRes.data, bytes, 0, bytes.Length);

                    //Allow Packet to be removed from Memory
                    Functions.rav1e_packet_unref(ptr);

                    //Send Frame
                    yield return (status, bytes);
                }
                else
                {
                    yield return (status, null);
                }
            }
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
    }
}
