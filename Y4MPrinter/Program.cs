using System.Collections;
using System.Drawing;
using System.Text;


string path = @"C:\Users\darkf\Desktop\output.y4m";

int width = 1920;
int height = 1080;
int sizeFrame = 1920 * 1080 * 3 / 2;

var headerSize = 0;
List<byte[]> frameBytes = new List<byte[]>();

//Read Header
using (var sr = new StreamReader(path))
{
    StringBuilder sb = new StringBuilder();
    var line = sr.ReadLine();
    Console.WriteLine(line);
    headerSize = line.Length;
}

//Read Whole file
using (Stream source = File.OpenRead(path))
{
    byte[] buffer = new byte[sizeFrame];
    int bytesRead = 0;
    source.Position = headerSize + 7;
    while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
    {
        var tmp = new byte[buffer.Length];
        Array.Copy(buffer, 0, tmp, 0, tmp.Length);
        frameBytes.Add(tmp);
        break;
    }
}

//Size of the Y Cb Cr section
int ySize = width * height;
int cbSize = ySize / 4;
int crSize = ySize / 4;

// Create separate byte arrays for Y, Cb, and Cr components
byte[] yComponent = new byte[ySize];
byte[] cbComponent = new byte[cbSize];
byte[] crComponent = new byte[crSize];

// Extract the Y, Cb, and Cr components from frameBytes[0]
Array.Copy(frameBytes[0], 0, yComponent, 0, ySize);
Array.Copy(frameBytes[0], ySize, cbComponent, 0, cbSize);
Array.Copy(frameBytes[0], ySize + cbSize, crComponent, 0, crSize);

byte[,] cbMap = new byte[width / 2, height / 2];
byte[,] crMap = new byte[width / 2, height / 2];

for (int x = 0; x < width / 2; x++)
{
    for(int y = 0; y < height / 2; y++)
    {
        var index = x + y * (width / 2);
        cbMap[x / 2, y / 2] = cbComponent[index];
        crMap[x / 2, y / 2] = crComponent[index];
    }
}

Bitmap bitmap = new Bitmap(width, height);



for (int x = 0; x < width; x++)
{
    for(int y = 0; y < height; y++)
    {
        // Get the Y, Cb, and Cr values of the pixel
        byte yValue = yComponent[x+y*width];
        byte cbValue = cbMap[x/4, y/4]; // Upsample Cb and Cr components
        byte crValue = crMap[x/4, y/4];

        Color color = RGBFromYCbCr(yValue, cbValue, crValue);

        bitmap.SetPixel(x, y, color);
    }
}

bitmap.Save("helloWorld.png");


static Color RGBFromYCbCr(byte y, byte cb, byte cr)
{
    double Y = (double)y;
    double Cb = (double)cb;
    double Cr = (double)cr;

    int r = (int)(Y + 1.40200 * (Cr - 0x80));
    int g = (int)(Y - 0.34414 * (Cb - 0x80) - 0.71414 * (Cr - 0x80));
    int b = (int)(Y + 1.77200 * (Cb - 0x80));

    r = Math.Max(0, Math.Min(255, r));
    g = Math.Max(0, Math.Min(255, g));
    b = Math.Max(0, Math.Min(255, b));

    return Color.FromArgb(r,g,b);
}
