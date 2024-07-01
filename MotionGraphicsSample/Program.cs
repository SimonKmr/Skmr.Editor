// See https://aka.ms/new-console-template for more information
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.MotionGraphics;
using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Structs;

Sequence seq = new Sequence(1920, 1080);

var txtTitle = new Text();

txtTitle.SourceText = "TITLE";
txtTitle.FontFile = @"C:\Windows\Fonts\Fontfabric - Nexa Black.otf";
txtTitle.TextSize = 100.0f;

txtTitle.Color.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 1,
        Transition = (Functions.Cubic),
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0x00)
    });

txtTitle.Color.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 40,
        Transition = ((x) => x),
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0xFF)
    });

txtTitle.Position.Keyframes.Add(
    new Keyframe<Pos2D>
    {
        Frame = 1,
        Transition = (Functions.Cubic),
        Value = new Pos2D(1920 / 2, 1080 / 4 - 130),
    });

txtTitle.Position.Keyframes.Add(
    new Keyframe<Pos2D>
    {
        Frame = 40,
        Transition = ((x) => x),
        Value = new Pos2D(1920 / 2, 1080 / 4 - 20),
    });

var txtVs = new Text();

txtVs.SourceText = "vs";
txtVs.FontFile = @"C:\Windows\Fonts\Fontfabric - Nexa Extra Light Italic.otf";

txtVs.Color.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 40,
        Transition = (Functions.Cubic),
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0x00)
    });

txtVs.Color.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 80,
        Transition = ((x) => x),
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0xFF)
    });

txtVs.Position.Keyframes.Add(
    new Keyframe<Pos2D>
    {
        Frame = 40,
        Transition = (Functions.Cubic),
        Value = new Pos2D(1920 / 2, 1080 / 2 - 32),
    });

txtVs.Position.Keyframes.Add(
    new Keyframe<Pos2D>
    {
        Frame = 80,
        Transition = ((x) => x),
        Value = new Pos2D(1920 / 2, 1080 / 2 + 32),
    });

var txtTeam01 = new Text();

txtTeam01.SourceText = "Mad Lions Koi";
txtTeam01.FontFile = @"C:\Windows\Fonts\Fontfabric - Nexa Bold.otf";

txtTeam01.Color.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 50,
        Transition = (Functions.Cubic),
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0x00)
    });

txtTeam01.Color.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 90,
        Transition = ((x) => x),
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0xFF)
    });

txtTeam01.Position.Keyframes.Add(
    new Keyframe<Pos2D>
    {
        Frame = 50,
        Transition = (Functions.Cubic),
        Value = new Pos2D(1920 / 4 - 50, 1080 / 3 * 2 + 100),
    });

txtTeam01.Position.Keyframes.Add(
    new Keyframe<Pos2D>
    {
        Frame = 90,
        Transition = ((x) => x),
        Value = new Pos2D(1920 / 4, 1080 / 3 * 2 + 100),
    });

var txtTeam02 = new Text();

txtTeam02.SourceText = "Fnatic";
txtTeam02.FontFile = @"C:\Windows\Fonts\Fontfabric - Nexa Bold.otf";

txtTeam02.Color.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 50,
        Transition = (Functions.Cubic),
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0x00)
    });

txtTeam02.Color.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 90,
        Transition = ((x) => x),
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0xFF)
    });

txtTeam02.Position.Keyframes.Add(
    new Keyframe<Pos2D>
    {
        Frame = 50,
        Transition = (Functions.Cubic),
        Value = new Pos2D(1920 / 4 * 3 + 50, 1080 / 3 * 2 + 100),
    });

txtTeam02.Position.Keyframes.Add(
    new Keyframe<Pos2D>
    {
        Frame = 90,
        Transition = ((x) => x),
        Value = new Pos2D(1920 / 4 * 3, 1080 / 3 * 2 + 100),
    });

var mdkLogo = new Image();

mdkLogo.ImagePath = @"C:\Users\darkf\OneDrive\Videos\MDK Documentary\images\Team Logos\white\mdk.png";

mdkLogo.Alpha.Keyframes.Add(
    new Keyframe<AByte>
    {
        Frame = 20,
        Transition = Functions.Cubic,
        Value = new AByte(0),
    });

mdkLogo.Alpha.Keyframes.Add(
    new Keyframe<AByte>
    {
        Frame = 60,
        Transition = Functions.Cubic,
        Value = new AByte(255),
    });

mdkLogo.Position.Keyframes.Add(
    new Keyframe<Pos2D>
    {
        Frame = 20,
        Transition = Functions.Cubic,
        Value = new Pos2D(
            1920 / 4 - 225, 
            1080 / 2 - 225 - 100),
    });

mdkLogo.Position.Keyframes.Add(
    new Keyframe<Pos2D>
    {
        Frame = 60,
        Transition = Functions.Linear,
        Value = new Pos2D(
            1920 / 4 - 225, 
            1080 / 2 - 225 - 50),
    });

var fncLogo = new Image();

fncLogo.ImagePath = @"C:\Users\darkf\OneDrive\Videos\MDK Documentary\images\Team Logos\white\fnatic.png";

fncLogo.Alpha.Keyframes.Add(
    new Keyframe<AByte>
    {
        Frame = 60,
        Transition = Functions.Cubic,
        Value = new AByte(0),
    });

fncLogo.Alpha.Keyframes.Add(
    new Keyframe<AByte>
    {
        Frame = 100,
        Transition = Functions.Cubic,
        Value = new AByte(255),
    });

fncLogo.Position.Keyframes.Add(
    new Keyframe<Pos2D>
    {
        Frame = 60,
        Transition = Functions.Cubic,
        Value = new Pos2D(
            1920 / 4 * 3 - 225,
            1080 / 2 - 225 - 100 + 50),
    });

fncLogo.Position.Keyframes.Add(
    new Keyframe<Pos2D>
    {
        Frame = 100,
        Transition = Functions.Linear,
        Value = new Pos2D(
            1920 / 4 * 3 - 225,
            1080 / 2 - 225 - 50 + 50),
    });

//seq.Elements.Add(imgMain);
seq.Elements.Add(txtTitle);
seq.Elements.Add(txtVs);
seq.Elements.Add(txtTeam01);
seq.Elements.Add(txtTeam02);
seq.Elements.Add(mdkLogo);
seq.Elements.Add(fncLogo);

for (int i = 0; i < 240; i++)
{
    using (var writer = new StreamWriter(new FileStream($"result\\{i:D5}.png",FileMode.Create)))
    {
        var bytes = seq.Render(i);
        writer.BaseStream.Write(bytes,0,bytes.Length);
    }
    Console.WriteLine($"{i:D5}");
}
