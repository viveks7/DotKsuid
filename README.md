# DotKsuid

A .NET Standard 2.0 port of [Segment's](https://segment.com/) implementation of [K-Sortable Unique IDentifiers(KSUID)](https://github.com/segmentio/ksuid). This library has been optimised for better performance and less memory utilization.

[![Build](https://github.com/viveks7/DotKsuid/actions/workflows/build.yml/badge.svg)](https://github.com/viveks7/DotKsuid/actions/workflows/build.yml)  [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=viveks7_DotKsuid&metric=alert_status)](https://sonarcloud.io/dashboard?id=viveks7_DotKsuid)  [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=viveks7_DotKsuid&metric=coverage)](https://sonarcloud.io/dashboard?id=viveks7_DotKsuid)

# Installation

To install from NuGet with `dotnet` CLI, use following command

```
dotnet add package DotKsuid --version 1.0.0-preview1
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
