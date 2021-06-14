using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Running;

namespace DotKsuid.Benchmarks
{
    [ExcludeFromCodeCoverage]
    static class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<KsuidPerfTests>();
        }
    }

}
