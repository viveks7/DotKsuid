using System;
using BenchmarkDotNet.Attributes;

namespace DotKsuid.Benchmarks
{
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

        //private Ksuid ksuid;
        //private KSUID.Ksuid _ksuid;

        //[GlobalSetup]
        //public void GlobalSetup()
        //{
        //    ksuid = Ksuid.NewKsuid();
        //    _ksuid = KSUID.Ksuid.Generate();
        //}

        //[Benchmark]
        //public string Test() => ksuid.ToString();

        //[Benchmark]
        //public string Test2() => _ksuid.ToString();
    }
}
