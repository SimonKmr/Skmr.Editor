// See https://aka.ms/new-console-template for more information
using Skmr.Editor.Images;
using Skmr.Editor.Images.Patterns;

ICommand[] commands =
{
    new Grid()
    {
        TileSize = 50,
        Color = (0,0, 0),
    }
};
Renderer.Save(1920 * 4, 1080 * 4, "grid_black.png", commands);
