using Skmr.Editor.Engine;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using SE = Skmr.Editor.Engine;


string path = @"C:\Users\darkf\Desktop\output - Kopie.y4m";
string path2 = @"C:\Users\darkf\Desktop\test.y4m";
string output = @"test2.ivf";

var test = new SE.Y4M(path);

// Create a config file and set width and height
var config = SE.Codecs.Rav1e.Api.rav1e_config_default();
var time_base = new SE.Codecs.Rav1e.Api.Rational() { num = 60, den = 1 };
Console.WriteLine("width:   " +      SE.Codecs.Rav1e.Api.rav1e_config_parse(config,"width", "1920")); //1920
Console.WriteLine("height:  " +      SE.Codecs.Rav1e.Api.rav1e_config_parse(config,"height", "1080")); //1080
SE.Codecs.Rav1e.Api.rav1e_config_set_time_base(config, time_base);

var configTest = Marshal.PtrToStructure<SE.Codecs.Rav1e.Api.Config>(config);

IntPtr configPtr = new IntPtr();
//Console.WriteLine(configTest.enc.time_base.num+" : "+ configTest.enc.time_base.den);
Marshal.StructureToPtr(configTest, configPtr, false);

//Creates a context out of the config
var context = SE.Codecs.Rav1e.Api.rav1e_context_new(configPtr);

//Console.WriteLine(SE.Codecs.Rav1e.Api.rav1e_rc_second_pass_data_required(context));
for(int i = 180; i < 240; i++)
{

    //https://dev.to/luzero/using-rav1e-from-your-own-code-2ie0
    //Get original frame
    var yuv = test.GetPlanes(i);

    //Creates a frame
    var frame = SE.Codecs.Rav1e.Api.rav1e_frame_new(context);

    //Create references of frame data
    GCHandle arr1 = GCHandle.Alloc(yuv[0], GCHandleType.Pinned);
    GCHandle arr2 = GCHandle.Alloc(yuv[1], GCHandleType.Pinned);
    GCHandle arr3 = GCHandle.Alloc(yuv[2], GCHandleType.Pinned);

    //move frame data into encoder frame
    //Stride => count of bytes till next line
    SE.Codecs.Rav1e.Api.rav1e_frame_fill_plane(frame, 0, arr1.AddrOfPinnedObject(), new IntPtr(yuv[0].Length), new IntPtr(1920), 1);
    SE.Codecs.Rav1e.Api.rav1e_frame_fill_plane(frame, 1, arr2.AddrOfPinnedObject(), new IntPtr(yuv[1].Length), new IntPtr(960), 1);
    SE.Codecs.Rav1e.Api.rav1e_frame_fill_plane(frame, 2, arr3.AddrOfPinnedObject(), new IntPtr(yuv[2].Length), new IntPtr(960), 1);
    
    //Appends Frame on queue
    SE.Codecs.Rav1e.Api.rav1e_send_frame(context, frame);

    arr1.Free();
    arr2.Free();
    arr3.Free();
}

//flush()
SE.Codecs.Rav1e.Api.rav1e_send_frame(context, IntPtr.Zero);

IntPtr ptr = IntPtr.Zero;
List<byte[]> result = new List<byte[]>();
do
{
    var status = SE.Codecs.Rav1e.Api.rav1e_receive_packet(context, ref ptr);
    
    if (status.Equals(SE.Codecs.Rav1e.Api.EncoderStatus.LimitReached)) break;
    if (status == SE.Codecs.Rav1e.Api.EncoderStatus.Encoded) continue;

    var pktRes = Marshal.PtrToStructure<SE.Codecs.Rav1e.Api.Packet>(ptr);
    if ((long)pktRes.frame_type > 10) continue;
    var len = (int)pktRes.len;
    var bytes = new byte[len];
    Marshal.Copy(pktRes.data, bytes, 0, bytes.Length);
    result.Add(bytes);

    SE.Codecs.Rav1e.Api.rav1e_packet_unref(ptr);
} while (true);

SE.Codecs.Rav1e.Api.rav1e_config_unref(config);
SE.Codecs.Rav1e.Api.rav1e_context_unref(context);


using (Stream source = File.Open(output, FileMode.Create))
{
    foreach(var e in result)
        source.Write(e, 0, e.Length);
}

 return;



var test2 = Y4M.Create(path2,1920,1080);

//var yuv = test.Get(1000);

//for (int i = 0; i < 200; i++)
//{
//    test2.Append();
//    var f = test.Get(i + 200);
//    test2.Set(i, f);
//}