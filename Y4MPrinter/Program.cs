using Skmr.Editor.Engine;
using System.Collections;
using System.Drawing;
using System.Text;
using SE = Skmr.Editor.Engine;


string path = @"C:\Users\darkf\Desktop\output.y4m";
string path2 = @"C:\Users\darkf\Desktop\test.y4m";
var test = new SE.Y4M(path);
var test2 = Y4M.Create(path2,1920,1080);

var frame = test.Get(1000);

for (int i = 0; i < 200; i++)
{
    test2.Append();
    var f = test.Get(i + 200);
    test2.Set(i, f);
}