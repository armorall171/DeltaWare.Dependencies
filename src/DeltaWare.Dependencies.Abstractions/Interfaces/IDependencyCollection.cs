using DeltaWare.Dependencies.Abstractions;
using System;

// ReSharper disable once CheckNamespace
namespace DeltaWare.Dependencies
{
    public interface IDependencyCollection: IDisposable
    {
        void AddDependency<TDependency>(Func<TDependency> dependency, Lifetime lifetime, Binding binding = Binding.Bound);

        void AddDependency<TDependency>(Func<IDependencyProvider, TDependency> dependency, Lifetime lifetime, Binding binding = Binding.Bound);

        bool TryAddDependency<TDependency>(Func<TDependency> dependency, Lifetime lifetime, Binding binding = Binding.Bound);

        bool TryAddDependency<TDependency>(Func<IDependencyProvider, TDependency> dependency, Lifetime lifetime, Binding binding = Binding.Bound);

        bool HasDependency<TDependency>();

        IDependencyProvider BuildProvider();

        IDependencyCollection Clone();
    }
}
