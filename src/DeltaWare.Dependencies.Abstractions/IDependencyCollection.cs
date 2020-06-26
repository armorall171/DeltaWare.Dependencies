using System;

namespace DeltaWare.Dependencies.Abstractions
{
    public interface IDependencyCollection
    {
        void AddDependency<TDependency>(Func<TDependency> dependency, Binding binding = Binding.Bound);

        bool TryAddDependency<TDependency>(Func<TDependency> dependency, Binding binding = Binding.Bound);

        IDependencyProvider BuildProvider();
    }
}
