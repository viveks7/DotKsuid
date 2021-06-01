using System;
using BenchmarkDotNet.Running;

namespace DotKsuid.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {

            BenchmarkRunner.Run<KsuidPerfTests>();
        }
    }

}
