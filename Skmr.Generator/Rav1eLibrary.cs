using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skmr.Generator
{
    public class Rav1eLibrary : ILibrary
    {
        public void Postprocess(Driver driver, ASTContext ctx) { }
        public void Preprocess(Driver driver, ASTContext ctx) { }
        public void Setup(Driver driver)
        {
            string path = @"C:\Users\darkf\source\repos\rav1e\target\x86_64-pc-windows-msvc\release";
            string name = "rav1e";

            var options = driver.Options;
            options.GeneratorKind = GeneratorKind.CSharp;
            var module = options.AddModule("Rav1e");
            module.IncludeDirs.Add(path);
            module.Headers.Add($"{name}.h");
            module.LibraryDirs.Add(path);
            module.Libraries.Add($"{name}.lib");
        }

        public void SetupPasses(Driver driver) { }

    }
}
