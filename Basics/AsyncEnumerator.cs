using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Basics
{
    public class AsyncEnumerator
    {
        [Fact]
        public async Task CanCreateAsyncGenerator()
        {
            var items = await ReturnWithDelay(new[] { 1, 2, 3 }, 100).ToArrayAsync();
            items.Should().BeEquivalentTo(new[] { 1, 2, 3 });
        }

        [Fact]
        public async Task CanUseCancellationToken_UsingAttribute()
        {
            var results = new List<int>();
            var cancellationTokenSource = new CancellationTokenSource();
            var asyncEnumerable = ReturnWithDelay(new[] { 1, 2, 3 }, 100);
            await foreach (var item in asyncEnumerable.WithCancellation(cancellationTokenSource.Token))
            {
                results.Add(item);
                cancellationTokenSource.Cancel();
            }
            results.Should().BeEquivalentTo(new[] { 1 });
        }

        [Fact]
        public async Task CanUseCancellationToken_UsingDirectParameter()
        {
            var results = new List<int>();
            var cancellationTokenSource = new CancellationTokenSource();
            await foreach (var item in ReturnWithDelay(new[] { 1, 2, 3 }, 100, cancellationTokenSource.Token))
            {
                results.Add(item);
                cancellationTokenSource.Cancel();
            }
            results.Should().BeEquivalentTo(new[] { 1 });
        }

        private async IA­syncEnumerable<T> ReturnWithDelay<T>(IEnumerable<T> items, int delay, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            foreach (var item in items)
            {
                try
                {
                    await Task.Delay(delay, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    yield break;
                }
                yield return item;
            }
        }


    }

    public static class AsyncExt
    {
        public static async Task<T[]> ToArrayAsync<T>(this IAsyncEnumerable<T> items)
        {
            // NOTE: dummy implementation, use reactive extensions/rx/etc instead for production code
            var result = new List<T>();
            await foreach (var item in items)
            {
                result.Add(item);
            }
            return result.ToArray();
        }
    }
}
