using Y4M = Skmr.Editor.Engine.Y4M;
using Skmr.Editor.Engine.Codecs.Apis.Rav1e;
using Skmr.Editor.Engine.Bitstreams.H264;
using Skmr.Editor.Engine.Codecs;
using H264 = Skmr.Editor.Engine.Bitstreams.H264;
using OpenH264Sample;
using System.Drawing;
using Skmr.Editor.Engine.Y4M;
using Skmr.Editor.Engine;
using System;
using Skmr.Editor.Engine.Containers.Mp4;

var file = new Mp4File("C:\\Users\\Simon\\Desktop\\test.mp4");
var atoms = file.Read();
var leafs = Mp4File.GetLeafAtoms(atoms);

//vmhd

while (true) ;