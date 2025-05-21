// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using Skmr.Editor.Data;
using Skmr.Editor.Data.Colors;
using Skmr.Editor.Images.Patterns;
using Skmr.Editor.MotionGraphics;
using Skmr.Editor.MotionGraphics.Attributes;
using Skmr.Editor.MotionGraphics.Elements;
using Skmr.Editor.MotionGraphics.Enums;
using Skmr.Editor.MotionGraphics.IO;
using Skmr.Editor.MotionGraphics.Patterns;
using Skmr.Editor.MotionGraphics.Sequences;
using Skmr.Editor.MotionGraphics.Structs;
using Skmr.Editor.MotionGraphics.Structs.Noise;

using Presets = Skmr.Editor.MotionGraphics.Presets;

(int w, int h) resolution = (1920, 1080);
var fontFolder = @"C:\Users\Simon\AppData\Local\Microsoft\Windows\Fonts\";


Sequence seq = new Sequence(resolution.w, resolution.h);

var txtTitle = new Text();

txtTitle.SourceText = "LCK 2025";
txtTitle.FontFile = fontFolder+@"Fontfabric - Nexa Black.otf";
txtTitle.TextSize = 80.0f;
txtTitle.HorizontalAlignment = HorizontalAlignment.Left;
txtTitle.VerticalAlignment = VerticalAlignment.Top;
//txtTitle.CustomAnimation = Presets.Text.LetterAnimation;

var txtTitleColor = new InterpolatedAttribute<RGBA>();
txtTitleColor.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 1,
        Transition = (Function.Cubic),
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0x00)
    });
txtTitleColor.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 40,
        Transition = Function.Linear,
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0xFF)
    });

txtTitle.Color = txtTitleColor;

var txtTitlePosition = new InterpolatedAttribute<Vec2D>();
txtTitlePosition.Keyframes.Add(
    new Keyframe<Vec2D>
    {
        Frame = 1,
        Transition = (Function.Cubic),
        Value = new Vec2D(100, 220),
    });

txtTitlePosition.Keyframes.Add(
    new Keyframe<Vec2D>
    {
        Frame = 40,
        Transition = Function.Linear,
        Value = new Vec2D(50, 220),
    });

txtTitle.Position = txtTitlePosition;

var txtVs = new Text();

txtVs.SourceText = "vs";
txtVs.FontFile = fontFolder + @"Fontfabric - Nexa Extra Light Italic.otf";
txtVs.TextSize = 20;
var txtVsColor = new InterpolatedAttribute<RGBA>();

txtVsColor.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 40,
        Transition = Function.Cubic,
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0x00)
    });

txtVsColor.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 80,
        Transition = Function.Linear,
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0xFF)
    });

txtVs.Color = txtVsColor;

var txtVsPosition = new InterpolatedAttribute<Vec2D>();

txtVsPosition.Keyframes.Add(
    new Keyframe<Vec2D>
    {
        Frame = 40,
        Transition = Function.Cubic,
        Value = new Vec2D(500 / 2, 300 + 20),
    });

txtVsPosition.Keyframes.Add(
    new Keyframe<Vec2D>
    {
        Frame = 80,
        Transition = Function.Linear,
        Value = new Vec2D(500 / 2,  300 ),
    });

txtVs.Position = txtVsPosition;

var txtTeam01 = new Text();
txtTeam01.TextSize = 40;
txtTeam01.SourceText = "T1";
txtTeam01.FontFile = fontFolder + @"Fontfabric - Nexa Bold.otf";
txtTeam01.HorizontalAlignment = HorizontalAlignment.Left;

var txtTeam01Color = new InterpolatedAttribute<RGBA>();

txtTeam01Color.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 50,
        Transition = Function.Cubic,
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0x00)
    });

txtTeam01Color.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 90,
        Transition = Function.Linear,
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0xFF)
    });

txtTeam01.Color = txtTeam01Color;

var txtTeam01Position = new InterpolatedAttribute<Vec2D>();

txtTeam01Position.Keyframes.Add(
    new Keyframe<Vec2D>
    {
        Frame = 50,
        Transition = Function.Cubic,
        Value = new Vec2D(25, resolution.h / 3 * 2 + 175),
    });

txtTeam01Position.Keyframes.Add(
    new Keyframe<Vec2D>
    {
        Frame = 90,
        Transition = Function.Linear,
        Value = new Vec2D(25, resolution.h / 3 * 2 + 175),
    });

txtTeam01.Position = txtTeam01Position;

var txtTeam02 = new Text();
txtTeam02.TextSize = 40;
txtTeam02.SourceText = "Gen.G";
txtTeam02.FontFile = fontFolder + @"Fontfabric - Nexa Bold.otf";
txtTeam02.HorizontalAlignment = HorizontalAlignment.Left;

var txtTeam02Color = new InterpolatedAttribute<RGBA>();

txtTeam02Color.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 50,
        Transition = (Function.Cubic),
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0x00)
    });

txtTeam02Color.Keyframes.Add(
    new Keyframe<RGBA>
    {
        Frame = 90,
        Transition =    Function.Linear,
        Value = new RGBA(0xFF, 0xFF, 0xFF, 0xFF)
    });

txtTeam02.Color = txtTeam02Color;

var txtTeam02Position = new InterpolatedAttribute<Vec2D>();

txtTeam02Position.Keyframes.Add(
    new Keyframe<Vec2D>
    {
        Frame = 50,
        Transition = Function.Cubic,
        Value = new Vec2D(175, resolution.h / 3 * 2 + 175),
    });

txtTeam02Position.Keyframes.Add(
    new Keyframe<Vec2D>
    {
        Frame = 90,
        Transition = Function.Linear,
        Value = new Vec2D(175, resolution.h / 3 * 2 + 175),
    });

txtTeam02.Position = txtTeam02Position;

var mdkLogo = new Image();
mdkLogo.HorizontalAlignment = HorizontalAlignment.Center;
mdkLogo.VerticalAlignment = VerticalAlignment.Center;
mdkLogo.ImagePath = @"C:\Users\Simon\OneDrive\Videos\MDK Documentary\images\Team Logos\white\mdk.png";


var mdkLogoAlpha = new InterpolatedAttribute<AByte>();
mdkLogoAlpha.Keyframes.Add(
    new Keyframe<AByte>
    {
        Frame = 20,
        Transition = Function.Cubic,
        Value = new AByte(0),
    });

mdkLogoAlpha.Keyframes.Add(
    new Keyframe<AByte>
    {
        Frame = 60,
        Transition = Function.Cubic,
        Value = new AByte(255),
    });

mdkLogo.Alpha = mdkLogoAlpha;

var mdkLogoPosition = new InterpolatedAttribute<Vec2D>();

mdkLogoPosition.Keyframes.Add(
    new Keyframe<Vec2D>
    {
        Frame = 20,
        Transition = Function.Cubic,
        Value = new Vec2D(
            resolution.w / 4,
            resolution.h / 2 - 50),
    });

mdkLogoPosition.Keyframes.Add(
    new Keyframe<Vec2D>
    {
        Frame = 60,
        Transition = Function.Linear,
        Value = new Vec2D(
            resolution.w / 4,
            resolution.h / 2),
    });

mdkLogo.Position = mdkLogoPosition;

var fncLogo = new Image();
fncLogo.HorizontalAlignment = HorizontalAlignment.Center;
fncLogo.VerticalAlignment = VerticalAlignment.Center;
fncLogo.ImagePath = @"C:\Users\Simon\OneDrive\Videos\MDK Documentary\images\Team Logos\white\fnatic.png";
var fncLogoAlpha = new InterpolatedAttribute<AByte>();

fncLogoAlpha.Keyframes.Add(
    new Keyframe<AByte>
    {
        Frame = 60,
        Transition = Function.Cubic,
        Value = new AByte(0),
    });

fncLogoAlpha.Keyframes.Add(
    new Keyframe<AByte>
    {
        Frame = 100,
        Transition = Function.Cubic,
        Value = new AByte(255),
    });

fncLogo.Alpha = fncLogoAlpha;

var fncLogoPosition = new InterpolatedAttribute<Vec2D>();

fncLogoPosition.Keyframes.Add(
    new Keyframe<Vec2D>
    {
        Frame = 60,
        Transition = Function.Cubic,
        Value = new Vec2D(
            175,
            resolution.h / 2 - 50),
    });

fncLogoPosition.Keyframes.Add(
    new Keyframe<Vec2D>
    {
        Frame = 100,
        Transition = Function.Linear,
        Value = new Vec2D(
            175,
            resolution.h / 2),
    });

fncLogo.Position = fncLogoPosition;

var mapDots = new DotMap();

var map00 = AMap.FromFile(@"C:\Users\Simon\OneDrive\Videos\MDK Documentary\images\Assets\00.png");
var map01 = AMap.FromFile(@"C:\Users\Simon\OneDrive\Videos\MDK Documentary\images\Assets\01.png");
var map02 = AMap.FromFile(@"C:\Users\Simon\OneDrive\Videos\MDK Documentary\images\Assets\02.png");

var mapDotsMap = new ProcedualAttribute<AMap>();
mapDotsMap.Generator = (x) => Perlin.CreateNoiseMap(1920, 1080, 256, (double)(x / 200));
mapDots.Map = mapDotsMap;
mapDots.ColorMin = new RGBA(0xFF, 0x20, 0x20, 0x40);
mapDots.ColorMax = new RGBA(0xFF, 0xC4, 0x74, 0xFF);

mapDots.Resolution = new StaticAttribute<Vec2D>(new Vec2D(1920, 1080));

mapDots.MinMaxSize = new StaticAttribute<Vec2D>(new Vec2D(-7, 12));

mapDots.Spaceing = new StaticAttribute<AInt>(new AInt(10));

var mapClr = new ColorMapGPU();
mapClr.Resolution = new Vec2D(resolution.w, resolution.h);
var mapClrMap = new ProcedualAttribute<AMap>();
mapClrMap.Generator = (x) => PerlinGPU.CreateNoiseMap(1920, 1080, (double)(x / 200), 256);

mapClr.Map = mapClrMap;
mapClr.Color1 = new StaticAttribute<RGBA>(new RGBA(0xFF, 0x20, 0x20, 0x40));
mapClr.Color2 = new StaticAttribute<RGBA>(new RGBA(0xFF, 0xC4, 0x74, 0xFF));

var line = new Line();
line.Points = [
    new StaticAttribute<Vec2D>(new Vec2D(0,0)),
    new StaticAttribute<Vec2D>(new Vec2D(1920, 0)),
    new StaticAttribute<Vec2D>(new Vec2D(1920, 1080)),
    new StaticAttribute<Vec2D>(new Vec2D(0, 1080)),
    new StaticAttribute<Vec2D>(new Vec2D(0, 100))
];
line.Color = new StaticAttribute<RGBA>(new RGBA(0xFF, 0xFF, 0xFF, 0xFF));
line.Width = new StaticAttribute<AFloat>(new AFloat(20));
var end = new InterpolatedAttribute<AFloat>();

end.Keyframes.Add(new Keyframe<AFloat>()
{
    Frame = 1,
    Transition = Function.Linear,
    Value = new AFloat(0)
});
end.Keyframes.Add(new Keyframe<AFloat>()
{
    Frame = 100,
    Transition = Function.Linear,
    Value = new AFloat(1)
});
line.End = end;

var start = new InterpolatedAttribute<AFloat>();

start.Keyframes.Add(new Keyframe<AFloat>()
{
    Frame = 20,
    Transition = Function.Linear,
    Value = new AFloat(0)
});
start.Keyframes.Add(new Keyframe<AFloat>()
{
    Frame = 120,
    Transition = Function.Linear,
    Value = new AFloat(1)
});
line.Start = start;

var gradient = new Gradient();
gradient.Position = new StaticAttribute<Vec2D>(new Vec2D(0, 0));
gradient.Resolution = new StaticAttribute<Vec2D>(new Vec2D(500, 1080));
gradient.GradientP1 = new StaticAttribute<Vec2D>(new Vec2D(0, 0));
gradient.GradientP2 = new StaticAttribute<Vec2D>(new Vec2D(500, 1080));
gradient.Colors = new IAttribute<RGBA>[]
{
    new StaticAttribute<RGBA>(new RGBA(0x00, 0x6A, 0xFE, 0xFF)),
    new StaticAttribute<RGBA>(new RGBA(0x00, 0x1F, 0x47, 0xFF))
};

var solid = new Solid();
solid.Position = new StaticAttribute<Vec2D>(new Vec2D(0, 0));
solid.Resolution = new StaticAttribute<Vec2D>(new Vec2D(1920, 1080));
solid.Color = new StaticAttribute<RGBA>(new RGBA(0xFF, 0xFF, 0xFF, 0xFF));

//seq.Elements.Add(imgMain);
//seq.Elements.Add(ptnGrid);
//seq.Elements.Add(mapClr);
//seq.Elements.Add(mapDots);
seq.Elements.Add(gradient);
seq.Elements.Add(txtTitle);
seq.Elements.Add(txtVs);
//seq.Elements.Add(txtTeam01);
//seq.Elements.Add(txtTeam02);
//seq.Elements.Add(mdkLogo);
//seq.Elements.Add(fncLogo);
//seq.Elements.Add(line);

var json = seq.ToJson();
var obj = Manager.FromJson(json);

var frames = 240;

obj.Encoding = Encoding.Png;

DateTime startTotal = DateTime.Now;
seq.EndFrame = frames;
var test = obj.RenderFrame(10);

using (var outImg = System.IO.File.Open(@$"result/test.png", FileMode.Create))
{
    outImg.Write(test);
}



return;
seq.FrameRendered = (i, bytes) =>
{
    DateTime start = DateTime.Now;
    using (var outImg = System.IO.File.Open(@$"result/{i:D5}.png", FileMode.Create))
    {
        outImg.Write(bytes);
    }
    DateTime stop = DateTime.Now;
};

seq.Render();


var totalTime = DateTime.Now - startTotal;
Console.WriteLine(
    $"avr. frame time {totalTime.TotalMilliseconds / frames} ms per frame\n" +
    $"total time: {totalTime.ToString("mm':'ss")} ");

return;

