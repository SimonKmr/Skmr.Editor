using CppSharp;
using Skmr.Generator;

Console.WriteLine("Generating Rav1e Bindings...");
Console.WriteLine();

ConsoleDriver.Run(new Rav1eLibrary());

Console.WriteLine();
Console.WriteLine("Generating openh264 Bindings...");
Console.WriteLine();