``` ini

BenchmarkDotNet=v0.11.0, OS=Windows 10.0.17686
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
Frequency=2531251 Hz, Resolution=395.0616 ns, Timer=TSC
.NET Core SDK=2.1.300
  [Host]       : .NET Core 2.1.0 (CoreCLR 4.6.26515.07, CoreFX 4.6.26515.06), 64bit RyuJIT
  BenchmarkRun : .NET Core 2.1.0 (CoreCLR 4.6.26515.07, CoreFX 4.6.26515.06), 64bit RyuJIT

Job=BenchmarkRun  IterationCount=5  LaunchCount=2  
RunStrategy=Throughput  WarmupCount=2  

```
|        Method |        Mean |      Error |     StdDev |      Gen 0 |   Allocated |
|-------------- |------------:|-----------:|-----------:|-----------:|------------:|
|           Run | 1,836.40 ms | 449.508 ms | 297.322 ms | 68000.0000 | 214403232 B |
| RunCreateOnce |    25.19 ms |   2.969 ms |   1.964 ms |          - |         0 B |
