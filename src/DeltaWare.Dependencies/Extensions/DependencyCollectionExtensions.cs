using DeltaWare.Dependencies.Abstractions;
using System;

// ReSharper disable once CheckNamespace
namespace DeltaWare.Dependencies
{
    public static class DependencyCollectionExtensions
    {
        public static void AddSingleton<TDependency>(this IDependencyCollection dependencyCollection, Func<TDependency> dependency, Binding binding = Binding.Bound)
        {
            dependencyCollection.AddDependency(dependency, Lifetime.Singleton, binding);
        }

        public static void AddSingleton<TDependency>(this IDependencyCollection dependencyCollection, Func<IDependencyProvider, TDependency> dependency, Binding binding = Binding.Bound)
        {
            dependencyCollection.AddDependency(dependency, Lifetime.Singleton, binding);
        }

        public static bool TryAddSingleton<TDependency>(this IDependencyCollection dependencyCollection, Func<TDependency> dependency, Binding binding = Binding.Bound)
        {
            return dependencyCollection.TryAddDependency(dependency, Lifetime.Singleton, binding);
        }

        public static bool TryAddSingleton<TDependency>(this IDependencyCollection dependencyCollection, Func<IDependencyProvider, TDependency> dependency, Binding binding = Binding.Bound)
        {
            return dependencyCollection.TryAddDependency(dependency, Lifetime.Singleton, binding);
        }

        public static void AddScoped<TDependency>(this IDependencyCollection dependencyCollection, Func<TDependency> dependency, Binding binding = Binding.Bound)
        {
            dependencyCollection.AddDependency(dependency, Lifetime.Scoped, binding);
        }

        public static void AddScoped<TDependency>(this IDependencyCollection dependencyCollection, Func<IDependencyProvider, TDependency> dependency, Binding binding = Binding.Bound)
        {
            dependencyCollection.AddDependency(dependency, Lifetime.Scoped, binding);
        }

        public static bool TryAddScoped<TDependency>(this IDependencyCollection dependencyCollection, Func<TDependency> dependency, Binding binding = Binding.Bound)
        {
            return dependencyCollection.TryAddDependency(dependency, Lifetime.Scoped, binding);
        }

        public static bool TryAddScoped<TDependency>(this IDependencyCollection dependencyCollection, Func<IDependencyProvider, TDependency> dependency, Binding binding = Binding.Bound)
        {
            return dependencyCollection.TryAddDependency(dependency, Lifetime.Scoped, binding);
        }

        public static void AddTransient<TDependency>(this IDependencyCollection dependencyCollection, Func<TDependency> dependency, Binding binding = Binding.Bound)
        {
            dependencyCollection.AddDependency(dependency, Lifetime.Transient, binding);
        }

        public static void AddTransient<TDependency>(this IDependencyCollection dependencyCollection, Func<IDependencyProvider, TDependency> dependency, Binding binding = Binding.Bound)
        {
            dependencyCollection.AddDependency(dependency, Lifetime.Transient, binding);
        }

        public static bool TryAddTransient<TDependency>(this IDependencyCollection dependencyCollection, Func<TDependency> dependency, Binding binding = Binding.Bound)
        {
            return dependencyCollection.TryAddDependency(dependency, Lifetime.Transient, binding);
        }

        public static bool TryAddTransient<TDependency>(this IDependencyCollection dependencyCollection, Func<IDependencyProvider, TDependency> dependency, Binding binding = Binding.Bound)
        {
            return dependencyCollection.TryAddDependency(dependency, Lifetime.Transient, binding);
        }
    }
}
