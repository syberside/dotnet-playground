using System;
using System.Buffers;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Basics
{
    public class SpansAndMemory
    {
        [Fact]
        public void CanWrap_SliceOfArray_And_ModifyValues()
        {
            var array = new int[] { 1, 2, 3, 4 };
            var slice = new Span<int>(array, 1, 2);
            AddTenToEachElement(slice);
            array.Should().BeEquivalentTo(new int[] { 1, 12, 13, 4 });
        }

        [Fact]
        void CanTake_SliceOfStack_And_ModifyValues()
        {
            Span<int> array = stackalloc int[] { 1, 2, 3, 4 };
            var slice = array.Slice(1, 2);
            AddTenToEachElement(slice);
            array.ToArray().Should().BeEquivalentTo(new int[] { 1, 12, 13, 4 });
        }

        [Fact]
        void CanUse_MemoryPool_ToRentManagedOwnedBuffer()
        {
            using (var owner = MemoryPool<int>.Shared.Rent(4))
            {
                var memory = owner.Memory;
                var slice = memory.Span.Slice(0, 4);
                for (var i = 0; i < slice.Length; i++)
                {
                    slice[i] = i + 1;
                }
                AddTenToEachElement(memory);
                slice.ToArray().Should().BeEquivalentTo(new int[] { 11, 12, 13, 14 });
            }
        }

        [Fact]
        void CanUse_OwnerlessMemory_ToManualyManageOwnedBuffer()
        {
            var str = " 1234 ";
            var memory = str.AsMemory();
            var result = ReadIntsFromStr(memory.Span).ToArray();
            result.Should().BeEquivalentTo(new[] { 0, 1, 2, 3, 4, 0 });
        }

        private int[] ReadIntsFromStr(ReadOnlySpan<char> span)
        {
            var results = new int[span.Length];
            for (var i = 0; i < span.Length; i++)
            {
                var ch = span[i];
                int.TryParse(ch.ToString(), out var result);
                results[i] = result;
            }
            return results;
        }

        private void AddTenToEachElement(Memory<int> memory)
        {
            var span = memory.Span;
            for (var i = 0; i < span.Length; i++)
            {
                span[i] += 10;
            }
        }

        private void AddTenToEachElement(Span<int> slice)
        {
            for (var i = 0; i < slice.Length; i++)
            {
                slice[i] += 10;
            }
        }
    }
}
