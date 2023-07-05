using Skmr.Editor.Engine;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using SE = Skmr.Editor.Engine;


string path = @"C:\Users\darkf\Desktop\output.y4m";
string path2 = @"C:\Users\darkf\Desktop\test.y4m";

var test = new SE.Y4M(path);

var version = SE.Codecs.Rav1e.rav1e_version_full();
var config = SE.Codecs.Rav1e.rav1e_config_default();
Console.WriteLine("width:  " + SE.Codecs.Rav1e.rav1e_config_parse(config,"width","1920"));
Console.WriteLine("height: " + SE.Codecs.Rav1e.rav1e_config_parse(config,"height","1080"));

var context = SE.Codecs.Rav1e.rav1e_context_new(config);
var frame = SE.Codecs.Rav1e.rav1e_frame_new(context);

var yuv = test.GetPlanes(1000);

GCHandle arr1 = GCHandle.Alloc(yuv[0], GCHandleType.Pinned);
GCHandle arr2 = GCHandle.Alloc(yuv[1], GCHandleType.Pinned);
GCHandle arr3 = GCHandle.Alloc(yuv[2], GCHandleType.Pinned);

//Stride => count of bytes till next line
SE.Codecs.Rav1e.rav1e_frame_fill_plane(frame, 0, arr1.AddrOfPinnedObject(), new IntPtr(yuv[0].Length), new IntPtr(1920), 1);
SE.Codecs.Rav1e.rav1e_frame_fill_plane(frame, 1, arr2.AddrOfPinnedObject(), new IntPtr(yuv[1].Length), new IntPtr(860), 1); 
SE.Codecs.Rav1e.rav1e_frame_fill_plane(frame, 2, arr3.AddrOfPinnedObject(), new IntPtr(yuv[2].Length), new IntPtr(860), 1);

var header = SE.Codecs.Rav1e.rav1e_container_sequence_header(context);

Console.WriteLine(SE.Codecs.Rav1e.rav1e_send_frame(context, frame));

SE.Codecs.Rav1e.rav1e_context_unref(context);
SE.Codecs.Rav1e.rav1e_config_unref(config);
Console.WriteLine(version);

return;



var test2 = Y4M.Create(path2,1920,1080);

//var yuv = test.Get(1000);

//for (int i = 0; i < 200; i++)
//{
//    test2.Append();
//    var f = test.Get(i + 200);
//    test2.Set(i, f);
//}