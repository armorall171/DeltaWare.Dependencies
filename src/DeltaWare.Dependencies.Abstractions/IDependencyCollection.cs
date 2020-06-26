using System;

namespace DeltaWare.Dependencies.Abstractions
{
    public interface IDependencyCollection: IReadOnlyDependencyCollection, IDisposable
    {
        void AddDependency<TDependency>(TDependency dependency, Binding binding = Binding.Bound);

        bool TryAddDependency<TDependency>(TDependency dependency, Binding binding = Binding.Bound);
    }
}
