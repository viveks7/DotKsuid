using System;
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;

namespace DotKsuid.Benchmarks
{
    [ExcludeFromCodeCoverage]
    [MemoryDiagnoser]
    public class KsuidPerfTests
    {
        [Benchmark]
        public Ksuid Ksuids() => Ksuid.NewKsuid();

        [Benchmark]
        public Guid Guids() => Guid.NewGuid();

        [Benchmark]
        public KSUID.Ksuid KsuidNet() => KSUID.Ksuid.Generate();

        [Benchmark]
        public string KsuidString() => Ksuid.NewKsuid().ToString();

        [Benchmark]
        public string GuidString() => Guid.NewGuid().ToString();

        [Benchmark]
        public string KsuidNetString() => KSUID.Ksuid.Generate().ToString();
    }
}
