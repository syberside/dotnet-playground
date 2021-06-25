using System;

namespace Basics
{
    public class Variance
    {
        private void Covariance_out(ICovariant<Base> baseVar, ICovariant<Derived> derivedVar)
        {
            baseVar = derivedVar;
            Base value = baseVar.Read();
            // Sample: List<T>, IEnumerable<T>, etc
            // It's safe to assign List<Derived> to List<Base>, because every where we expect Base we can use Derived
        }

        private void Contrvariance_in(IContrvariance<Base> baseVar, IContrvariance<Derived> derivedVar)
        {
            derivedVar = baseVar;
            derivedVar.Write(new Derived());
            // Sample: Action<T>
            // Action<Base> baseVar = (target) => { Console.WriteLine(target.GetType().Name); };
            // Action<Derived> derivedVar = baseVar;
            // derivedVar(new Derived());
            // It's safe because we are waiting for something that is assignable to Base as an argument
            // We make generic argument more strict by assigning Action<Base> to Action<Derived>
            // derivedVar(new Base()) - this will not work
        }

        private void Both_Sample()
        {
            Func<Base, Derived> f1 = (Base x) => new Derived();
            // Covariant return type. Here we can use return value as more abstract
            Func<Base, Base> f2 = f1;
            Base b2 = f2(new Base());

            // Contravariant parameter type. Here we can use more concret value as a parameter
            Func<Derived, Derived> f3 = f1;
            Derived d3 = f3(new Derived());
        }

        private interface ICovariant<out T>
        {
            T Read() => default(T);
        }

        private interface IContrvariance<in T>
        {
            void Write(T instance) => Console.WriteLine(instance.GetType().Name);
        }

        private class Base { }

        private class Derived : Base { }
    }
}
