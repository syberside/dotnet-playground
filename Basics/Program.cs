using System;

namespace Basics
{
    class Program
    {
        static void Main(string[] args)
        {
            RefKeyword();
        }

        private static void RefKeyword()
        {
            var integer = 5;
            Console.WriteLine(integer);
            ModifyValueType(ref integer); // Will change value
            Console.WriteLine(integer);

            var str = "Hello";
            Console.WriteLine(str);
            ModifyString(str); // No changes
            Console.WriteLine(str);

            Console.WriteLine(str);
            ModifyStringByRef(ref str); // Will change value
            Console.WriteLine(str);

            var data = new Data { Content = "Content" };
            Console.WriteLine(data.Content);
            ModifyReferenceType(data); // No changes observable from this method
            Console.WriteLine(data.Content);

            Console.WriteLine(data.Content);
            ModifyReferenceTypeByRef(ref data); // Will change variable value
            Console.WriteLine(data.Content);

            var intData = new IntData();
            var value = intData.GetValue();
            Console.WriteLine(value);
            value += 10; // No changes
            intData.PrintValue();

            ref var refValue = ref intData.GetValue();
            Console.WriteLine(refValue);
            refValue += 10; //Will change source variable (field)
            intData.PrintValue();

            Console.WriteLine();
            var structOnStack = new Struct
            {
                Value = 42
            };
            IStruct structOnHeap = structOnStack;
            DoUpdate(structOnHeap); // Will pass by ref (change)
            Console.WriteLine($"Update onHeap: stack={structOnStack.Value} - heap={structOnHeap.Value}");
            DoUpdate(structOnStack); // Will pass by stack (no changes)
            Console.WriteLine($"Update onStack: stack={structOnStack.Value} - heap={structOnHeap.Value}");

            DoUpdate<IStruct>(structOnHeap); // Will pass by ref (change)
            Console.WriteLine($"Update onHeap (generic): stack={structOnStack.Value} - heap={structOnHeap.Value}");
            DoUpdate<IStruct>(structOnStack); // Will pass by stack (no changes)
            Console.WriteLine($"Update onStack (generic): stack={structOnStack.Value} - heap={structOnHeap.Value}");
            DoUpdate<Struct>(structOnStack); // Will pass by stack (no changes) because of Update inside method
            Console.WriteLine($"Update onStack (generic): stack={structOnStack.Value} - heap={structOnHeap.Value}");
        }

        private static void ModifyReferenceType(Data data)
        {
            data = new Data { Content = "Not replaced Ref" };
        }

        private static void ModifyReferenceTypeByRef(ref Data data)
        {
            data = new Data { Content = "Replaced Ref" };
        }

        private static void ModifyValueType(ref int integer)
        {
            integer += 10;
        }

        private static void ModifyString(string str)
        {
            str = "Bye";
        }

        private static void ModifyStringByRef(ref string str)
        {
            str = "Bye";
        }

        private class Data
        {
            public String Content { get; set; }
        }

        private class IntData
        {
            private int _value = 42;

            public ref int GetValue() => ref _value;

            public void PrintValue()
            {
                Console.WriteLine(_value);
            }
        }

        private struct Struct : IStruct
        {
            public int Value { get; set; }

            public void Update()
            {
                Value += 10;
            }
        }

        private interface IStruct
        {
            void Update();
            int Value { get; }
        }

        private static void DoUpdate(IStruct str)
        {
            str.Update();
            Console.WriteLine(str.Value);
        }

        private static void DoUpdate<T>(T val) where T : IStruct
        {
            val.Update();
            Console.WriteLine(val.Value);
        }
    }
}
