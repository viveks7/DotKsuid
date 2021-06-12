using BenchmarkDotNet.Running;

namespace DotKsuid.Benchmarks
{
    static class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<KsuidPerfTests>();
        }
    }

}
