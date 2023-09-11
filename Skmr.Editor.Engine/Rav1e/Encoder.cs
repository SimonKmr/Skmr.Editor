using System.Runtime.InteropServices;

namespace Skmr.Editor.Engine.Rav1e
{
    public class Encoder : IDisposable
    {
        private RaContext context;
        private RaConfig config;

        private int width;
        private int height;
        private int fps;

        public Encoder(int width, int height, int fps = 30)
        {
            this.width = width;
            this.height = height;
            this.fps = fps;
            
            //Create a config file and set width and height
            config = Api.Rav1eConfigDefault();
            Api.Rav1eConfigParse(config, "width", width.ToString()); //1920
            Api.Rav1eConfigParse(config, "height", height.ToString()); //1080
            
            //Set Framerate
            var time_base = new RaRational() { Num = 1, Den = Convert.ToUInt64(fps) };
            Api.Rav1eConfigParse(config, "enable_timing_info", "true"); //true
            Api.Rav1eConfigSetTimeBase(config, time_base);
            
            //Creates a context out of the config
            context = Api.Rav1eContextNew(config);
        }

        public RaEncoderStatus SendFrame(Y4M.Frame input)
        {
            //Creates a frame
            var frame = Api.Rav1eFrameNew(context);
            var y = input.Get(Y4M.Channel.Y);
            var cb = input.Get(Y4M.Channel.Cb);
            var cr = input.Get(Y4M.Channel.Cr);


            unsafe
            {
                fixed (byte* ya = y, cba = cb, cra = cr)
                {
                    //Create references of frame data
                    //move frame data into encoder frame
                    //Stride => count of bytes till next line
                    Api.Rav1eFrameFillPlane(frame, 0, ya, (ulong)y.Length, width, 1);
                    Api.Rav1eFrameFillPlane(frame, 1, cba, (ulong)cb.Length, width / 2, 1);
                    Api.Rav1eFrameFillPlane(frame, 2, cra, (ulong)cr.Length, width / 2, 1);
                }
            }

            //Appends Frame on queue
            var status = Api.Rav1eSendFrame(context, frame);
            Api.Rav1eFrameUnref(frame);
            //Free arrays 

            return status;
        }

        public (RaEncoderStatus status, byte[]? data) ReceiveFrame()
        {
            while (true)
            {
                //Request Packet
                RaPacket packet = new RaPacket();
                var status = Api.Rav1eReceivePacket(context, packet);

                //Check if Packet is usable
                if (status.Equals(RaEncoderStatus.RA_ENCODER_STATUS_LIMIT_REACHED)) return (status,null);
                
                //If Decoding was successful
                if (status == RaEncoderStatus.RA_ENCODER_STATUS_SUCCESS)
                {
                    unsafe
                    {
                        var bytes = Create(packet.Data, (int)packet.Len);
                        //Allow Packet to be removed from Memory
                        Api.Rav1ePacketUnref(packet);
                        //Send Frame
                        return (status, bytes);
                    }
                }
                else
                {
                    return (status, null);
                }
            }
        }

        public void Flush()
        {
            Api.Rav1eSendFrame(context, null);
        }

        public void Dispose()
        {
            Api.Rav1eConfigUnref(config);
            Api.Rav1eContextUnref(context);
        }

        private unsafe static T[] Create<T>(T* ptr, int length) where T : unmanaged
        {
            T[] array = new T[length];
            for (int i = 0; i < length; i++)
                array[i] = ptr[i];
            return array;
        }
    }
}
