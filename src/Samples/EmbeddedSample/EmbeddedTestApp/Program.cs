using System;
using UseEmbeddedProjection;
using UseGlobalProjection;

namespace EmbeddedTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            UseEmbeddedProjection.EmbeddedScope embeddedScope = new();
            UseGlobalProjection.GlobalScope globalScope = new();
            var embeddedMap = embeddedScope.EmbeddedMapType;
            var globalMap = globalScope.GlobalStringMap;
            Console.WriteLine(embeddedMap["embedded"] + "=? scope");
            Console.WriteLine(globalMap["global"] + "=? scope");

            // embeddedScope.EmbeddedAlpha(globalMap); // BAD 
            embeddedScope.EmbeddedAlpha(embeddedMap); // GOOD

            globalScope.GlobalAlpha(globalMap); // GOOD
            // globalScope.GlobalAlpha(embeddedMap); // BAD

            // Windows.Foundation.Collections.StringMap newMap = new();
            // if cswinrt tool (code_writers) wrote everything as internal then this would not be a problem (then, StringMap would not be exposed by TestEmbedded.dll)
        }
    }
}
