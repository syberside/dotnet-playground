using System;
using System.IO;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Tasks
{
    [MemoryDiagnoser]
    [RPlotExporter]
    [CsvMeasurementsExporter]
    public class ValueTaskVSTask
    {
        private const int N = 1024;
        private readonly Random _random;

        private readonly byte[] _data;

        public ValueTaskVSTask()
        {
            _data = new byte[N];
            _random = new Random(42);
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _random.NextBytes(_data);
            File.WriteAllBytes("tmp.data", _data);
        }


        [GlobalCleanup]
        public void GlobalCleanup()
        {
            File.Delete("tmp.data");
        }

        [Benchmark]
        public async ValueTask ValueTaskSync() => await RunSync(() => ValueTask.CompletedTask);

        [Benchmark]
        public async Task TaskSync() => await RunSync(() => Task.CompletedTask);

        [Benchmark]
        public async ValueTask<bool> ValueTaskOfBoolSync() => await RunSync(() => ValueTask.FromResult(_random.Next(1) == 0));

        [Benchmark]
        public async Task<bool> TaskOfBoolSync() => await RunSync(() => Task.FromResult(_random.Next(1) == 0));

        [Benchmark]
        public async ValueTask<int> ValueTaskOfIntSync() => await RunSync(() => ValueTask.FromResult(_random.Next(N)));

        [Benchmark]
        public async Task<int> TaskOfIntSync() => await RunSync(() => Task.FromResult(_random.Next(N)));

        [Benchmark]
        public async ValueTask ValueTaskAsync() => await RunAsync(() => ValueTask.CompletedTask);

        [Benchmark]
        public async Task TaskAsync() => await RunAsync(() => Task.CompletedTask);

        [Benchmark]
        public async ValueTask<bool> ValueTaskOfBoolAsync() => await RunAsync(() => ValueTask.FromResult(_random.Next(1) == 0));

        [Benchmark]
        public async Task<bool> TaskOfBoolAsync() => await RunAsync(() => Task.FromResult(_random.Next(1) == 0));

        [Benchmark]
        public async ValueTask<int> ValueTaskOfIntAsync() => await RunAsync(() => ValueTask.FromResult(_random.Next(N)));

        [Benchmark]
        public async Task<int> TaskOfIntAsync() => await RunAsync(() => Task.FromResult(_random.Next(N)));

        [Benchmark]
        public async ValueTask<int> ValueTaskOfIntAsync_ValueTaskPayload() => await RunAsyncWithValueTaskPayload(() => ValueTask.FromResult(_random.Next(N)));

        [Benchmark]
        public async Task<int> TaskOfIntAsync_TaskPayload() => await RunAsyncWithTaskPayload(() => Task.FromResult(_random.Next(N)));

        private async ValueTask RunSync(Func<ValueTask> factory)
        {
            for (var i = 0; i < N; i++)
            {
                await factory();
            }
        }
        private async Task RunSync(Func<Task> factory)
        {
            for (var i = 0; i < N; i++)
            {
                await factory();
            }
        }

        private async ValueTask<T> RunSync<T>(Func<ValueTask<T>> factory)
        {
            T result = default(T);
            for (var i = 0; i < N; i++)
            {
                result = await factory();
            }
            return result;
        }
        private async Task<T> RunSync<T>(Func<Task<T>> factory)
        {
            T result = default(T);
            for (var i = 0; i < N; i++)
            {
                result = await factory();
            }
            return result;
        }

        private async ValueTask RunAsync(Func<ValueTask> factory)
        {
            for (var i = 0; i < N; i++)
            {
                await factory();
            }
            // NOTE: Task here - ok 
            await File.ReadAllBytesAsync("tmp.data");
        }
        private async Task RunAsync(Func<Task> factory)
        {
            for (var i = 0; i < N; i++)
            {
                await factory();
            }
            await File.ReadAllBytesAsync("tmp.data");
        }

        private async ValueTask<T> RunAsync<T>(Func<ValueTask<T>> factory)
        {
            T result = default(T);
            for (var i = 0; i < N; i++)
            {
                result = await factory();
            }
            // NOTE: Task here - ok 
            await File.ReadAllBytesAsync("tmp.data");
            return result;
        }
        private async Task<T> RunAsync<T>(Func<Task<T>> factory)
        {
            T result = default(T);
            for (var i = 0; i < N; i++)
            {
                result = await factory();
            }
            await File.ReadAllBytesAsync("tmp.data");
            return result;
        }

        private async ValueTask<T> RunAsyncWithValueTaskPayload<T>(Func<ValueTask<T>> factory)
        {
            T result = default(T);
            for (var i = 0; i < N; i++)
            {
                result = await factory();
            }
            using (var stream = File.Open("tmp.data", FileMode.Open))
            {
                ValueTask<int> t = stream.ReadAsync(new Memory<byte>(_data));
                await t;
            }
            return result;
        }

        private async Task<T> RunAsyncWithTaskPayload<T>(Func<Task<T>> factory)
        {
            T result = default(T);
            for (var i = 0; i < N; i++)
            {
                result = await factory();
            }
            using (var stream = File.Open("tmp.data", FileMode.Open))
            {
                Task<int> t = stream.ReadAsync(_data, 0, N);
                await t;
            }
            return result;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ValueTaskVSTask>();
        }
    }
}
