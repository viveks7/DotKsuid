# DotKsuid

A .NET Standard 2.0 port of [Segment's](https://segment.com/) implementation of [K-Sortable Unique IDentifiers(KSUID)](https://github.com/segmentio/ksuid). You can use this instead of GUID's where you can take advantage of it's

  * Natural ordering by generation time
  * Collision-free, coordination-free & dependency-free nature
  * Highly portable representation

[![Build](https://github.com/viveks7/DotKsuid/actions/workflows/build.yml/badge.svg)](https://github.com/viveks7/DotKsuid/actions/workflows/build.yml)  [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=viveks7_DotKsuid&metric=alert_status)](https://sonarcloud.io/dashboard?id=viveks7_DotKsuid)  [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=viveks7_DotKsuid&metric=coverage)](https://sonarcloud.io/dashboard?id=viveks7_DotKsuid)

# Installation

To install from NuGet with `dotnet` CLI, use following command

```
dotnet add package DotKsuid --version 1.0.0
```

# Usage

To create a new KSUID

```
var ksuid = Ksuid.NewKsuid();
```

To convert a KSUID to it's base62 encoded string representation

```
var ksuidString = Ksuid.NewKsuid().ToString();
```

# Performance

While this can't achieve the same performance as framework supported GUID's (since they are generated by calling native OS methods), it is written to be as fast as possible. Below is a benchmark result from the included Benchmarks project aganist GUID's

Screenshot 2021-06-18 at 10.14.40 PM<img width="748" alt="Screenshot 2021-06-18 at 10 14 40 PM" src="https://user-images.githubusercontent.com/8578039/122634715-63869600-d0fd-11eb-9258-418bd86ae258.png">

Below is a benchmark aganist the only other port of KSUID's available currently for .NET (tagged as "Ksuids" & "KsuidString").

Screenshot 2021-06-18 at 10.22.04 PM<img width="750" alt="Screenshot 2021-06-18 at 10 22 04 PM" src="https://user-images.githubusercontent.com/8578039/122634740-b6604d80-d0fd-11eb-9b8c-6947ccb2efab.png">

As you can see this solution has a vastly improved performance compared to other existing implementations.

