using Skmr.Editor.Engine.Containers.Mp4;

var file = new Mp4File("C:\\Users\\Simon\\Desktop\\test.mp4");
var atoms = file.Read();
var leafs = Mp4File.GetLeafAtoms(atoms);

//vmhd

while (true) ;