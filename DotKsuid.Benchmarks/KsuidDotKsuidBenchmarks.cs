using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;

namespace DotKsuid.Benchmarks
{
    [ExcludeFromCodeCoverage]
    [MemoryDiagnoser]
    public class KsuidDotKsuidBenchmarks
    {
        [Benchmark]
        public Ksuid DotKsuid() => Ksuid.NewKsuid();

        [Benchmark]
        public KSUID.Ksuid Ksuids() => KSUID.Ksuid.Generate();

        [Benchmark]
        public string DotKsuidString() => Ksuid.NewKsuid().ToString();

        [Benchmark]
        public string KsuidString() => KSUID.Ksuid.Generate().ToString();
    }
}
