``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.18363.1440 (1909/November2019Update/19H2)
Intel Core i5-10310U CPU 1.70GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=5.0.201
  [Host]     : .NET 5.0.4 (5.0.421.11614), X64 RyuJIT
  DefaultJob : .NET 5.0.4 (5.0.421.11614), X64 RyuJIT


```
|                               Method |       Mean |      Error |     StdDev |     Median |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------- |-----------:|-----------:|-----------:|-----------:|--------:|------:|------:|----------:|
|                        ValueTaskSync |   8.427 μs |  0.1025 μs |  0.0958 μs |   8.486 μs |       - |     - |     - |         - |
|                             TaskSync |   2.971 μs |  0.0586 μs |  0.1156 μs |   2.957 μs |       - |     - |     - |         - |
|                  ValueTaskOfBoolSync |  19.229 μs |  0.5106 μs |  1.4732 μs |  18.773 μs |       - |     - |     - |      64 B |
|                       TaskOfBoolSync |  23.168 μs |  0.4336 μs |  0.9697 μs |  22.790 μs | 23.4985 |     - |     - |  73,792 B |
|                   ValueTaskOfIntSync |  17.958 μs |  0.3568 μs |  0.3818 μs |  17.917 μs |       - |     - |     - |      64 B |
|                        TaskOfIntSync |  23.254 μs |  0.4625 μs |  0.8687 μs |  23.318 μs | 23.5596 |     - |     - |  73,935 B |
|                       ValueTaskAsync | 322.125 μs | 10.0985 μs | 28.9745 μs | 309.533 μs |  0.4883 |     - |     - |   2,360 B |
|                            TaskAsync | 295.928 μs |  4.0622 μs |  3.7998 μs | 296.172 μs |  0.4883 |     - |     - |   2,344 B |
|                 ValueTaskOfBoolAsync | 310.170 μs |  2.3998 μs |  2.1274 μs | 310.737 μs |  0.4883 |     - |     - |   2,448 B |
|                      TaskOfBoolAsync | 326.028 μs |  6.2146 μs | 12.9722 μs | 322.066 μs | 23.9258 |     - |     - |  76,143 B |
|                  ValueTaskOfIntAsync | 313.305 μs |  6.1243 μs |  7.2905 μs | 309.193 μs |  0.4883 |     - |     - |   2,448 B |
|                       TaskOfIntAsync | 317.388 μs |  2.8295 μs |  2.2091 μs | 317.851 μs | 23.9258 |     - |     - |  76,144 B |
| ValueTaskOfIntAsync_ValueTaskPayload | 298.205 μs |  5.6274 μs |  6.6990 μs | 296.098 μs |  1.4648 |     - |     - |   5,240 B |
|           TaskOfIntAsync_TaskPayload | 303.175 μs |  3.9397 μs |  3.6852 μs | 301.809 μs | 26.3672 |     - |     - |  78,928 B |
