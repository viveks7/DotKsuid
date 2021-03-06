using System;
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;

namespace DotKsuid.Benchmarks
{
    [ExcludeFromCodeCoverage]
    [MemoryDiagnoser]
    public class KsuidGuidBenchmarks
    {
        [Benchmark]
        public Ksuid DotKsuid() => Ksuid.NewKsuid();

        [Benchmark]
        public Guid Guids() => Guid.NewGuid();

        [Benchmark]
        public string DotKsuidString() => Ksuid.NewKsuid().ToString();

        [Benchmark]
        public string GuidString() => Guid.NewGuid().ToString();
    }
}
