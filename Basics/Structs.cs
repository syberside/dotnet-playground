using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace Basics
{
    public class Structs
    {
        [Fact]
        public void PassingByRef_AllowModification()
        {
            var integer = 42;
            AddTenToInteger(ref integer);
            integer.Should().Be(52);
        }

        private static void AddTenToInteger(ref int integer)
        {
            integer += 10;
        }

        [Fact]
        public void RefTypesPointer_WithoutRef_CantBeChanged()
        {
            var str = "Source";
            ChangeStringWithoutRef(str);
            str.Should().Be("Source");
        }

        private static void ChangeStringWithoutRef(string str)
        {
            str = "Changed";
        }

        [Fact]
        public void RefTypesPointer_WithRef_CanBeChanged()
        {
            var str = "Source";
            ChangeStringWithRef(ref str);
            str.Should().Be("Changes");
        }

        private static void ChangeStringWithRef(ref string str)
        {
            str = "Changes";
        }

        [Fact]
        public void StructReturnedByRef_WithoutRefReceiver_CantBeChanged()
        {
            var intData = new IntData();
            var value = intData.GetValue();
            value += 10;
            intData.GetValue().Should().Be(42);
        }

        [Fact]
        public void StructReturnedByRef_WithRefReceiver_CanBeChanged()
        {
            var intData = new IntData();
            ref var value = ref intData.GetValue();
            value += 10;
            intData.GetValue().Should().Be(52);
        }

        [Fact]
        public void Struct_OnCastToInterface_IsBoxed_And_CanBeChanged()
        {
            var structOnStack = new Struct
            {
                Value = 42
            };
            IStruct structOnHeap = structOnStack;
            AddTenToValue(structOnHeap);
            structOnStack.Value.Should().Be(42);
            structOnHeap.Value.Should().Be(52);
        }

        private struct Struct : IStruct
        {
            public int Value { get; set; }

            public void AddTen()
            {
                Value += 10;
            }
        }

        private interface IStruct
        {
            void AddTen();
            int Value { get; }
        }


        private static void AddTenToValue(IStruct str)
        {
            str.AddTen();
        }

        private class IntData
        {
            private int _value = 42;

            public ref int GetValue() => ref _value;
        }
    }
}
