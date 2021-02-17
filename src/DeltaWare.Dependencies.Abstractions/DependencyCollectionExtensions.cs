using System;
using System.Diagnostics.CodeAnalysis;

namespace DeltaWare.Dependencies.Abstractions
{
    public static class DependencyCollectionExtensions
    {
        /// <summary>
        /// Adds the specified dependency as a singleton instance. Overrides existing instances.
        /// </summary>
        /// <typeparam name="TDependency">Specifies the dependencies type.</typeparam>
        /// <param name="dependency">Specifies how to instantiate the dependency, including a provider to get existing dependencies.</param>
        /// <param name="binding">Specifies the binding of a dependency.</param>
        /// <exception cref="ArgumentNullException">Thrown when a null value is provided.</exception>
        public static void AddSingleton<TDependency>([NotNull] this IDependencyCollection collection, [NotNull] Func<TDependency> dependency, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.AddDependency(dependency, Lifetime.Singleton, binding);
        }

        /// <summary>
        /// Adds the specified dependency as a singleton instance. Overrides existing instances.
        /// </summary>
        /// <typeparam name="TDependency">Specifies the dependencies type.</typeparam>
        /// <param name="dependency">Specifies the dependency whilst supplying a provided to get an existing dependency.</param>
        /// <param name="binding">Specifies the binding of a dependency.</param>
        /// <exception cref="ArgumentNullException">Thrown when a null value is provided.</exception>
        public static void AddSingleton<TDependency>([NotNull] this IDependencyCollection collection, [NotNull] Func<IDependencyProvider, TDependency> dependency, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.AddDependency(dependency, Lifetime.Singleton, binding);
        }

        public static void AddSingleton<TDependency, TImplementation>([NotNull] this IDependencyCollection collection, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.AddDependency<TDependency, TImplementation>(Lifetime.Singleton, binding);
        }

        /// <summary>
        /// Adds the specified dependency as a singleton instance. Returns a <see cref="bool"/> specifying if the instance was added.
        /// </summary>
        /// <typeparam name="TDependency">Specifies the dependencies type.</typeparam>
        /// <param name="dependency">Specifies how to instantiate the dependency, including a provider to get existing dependencies.</param>
        /// <param name="binding">Specifies the binding of a dependency.</param>
        /// <exception cref="ArgumentNullException">Thrown when a null value is provided.</exception>
        /// <remarks>Only adds the dependency if no pre-existing instances are found.</remarks>
        public static bool TryAddSingleton<TDependency>([NotNull] this IDependencyCollection collection, [NotNull] Func<TDependency> dependency, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.TryAddDependency(dependency, Lifetime.Singleton, binding);
        }

        /// <summary>
        /// Adds the specified dependency as a singleton instance. Returns a <see cref="bool"/> specifying if the instance was added.
        /// </summary>
        /// <typeparam name="TDependency">Specifies the dependencies type.</typeparam>
        /// <param name="dependency">Specifies the dependency whilst supplying a provided to get an existing dependency.</param>
        /// <param name="binding">Specifies the binding of a dependency.</param>
        /// <exception cref="ArgumentNullException">Thrown when a null value is provided.</exception>
        /// <remarks>Only adds the dependency if no pre-existing instances are found.</remarks>
        public static bool TryAddSingleton<TDependency>([NotNull] this IDependencyCollection collection, [NotNull] Func<IDependencyProvider, TDependency> dependency, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.TryAddDependency(dependency, Lifetime.Singleton, binding);
        }

        public static bool TryAddSingleton<TDependency, TImplementation>([NotNull] this IDependencyCollection collection, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.TryAddDependency<TDependency, TImplementation>(Lifetime.Singleton, binding);
        }

        /// <summary>
        /// Adds the specified dependency as a scoped instance. Overrides existing instances.
        /// </summary>
        /// <typeparam name="TDependency">Specifies the dependencies type.</typeparam>
        /// <param name="dependency">Specifies how to instantiate the dependency, including a provider to get existing dependencies.</param>
        /// <param name="binding">Specifies the binding of a dependency.</param>
        /// <exception cref="ArgumentNullException">Thrown when a null value is provided.</exception>
        public static void AddScoped<TDependency>([NotNull] this IDependencyCollection collection, [NotNull] Func<TDependency> dependency, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.AddDependency(dependency, Lifetime.Scoped, binding);
        }

        /// <summary>
        /// Adds the specified dependency as a scoped instance. Overrides existing instances.
        /// </summary>
        /// <typeparam name="TDependency">Specifies the dependencies type.</typeparam>
        /// <param name="dependency">Specifies the dependency whilst supplying a provided to get an existing dependency.</param>
        /// <param name="binding">Specifies the binding of a dependency.</param>
        /// <exception cref="ArgumentNullException">Thrown when a null value is provided.</exception>
        public static void AddScoped<TDependency>([NotNull] this IDependencyCollection collection, [NotNull] Func<IDependencyProvider, TDependency> dependency, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.AddDependency(dependency, Lifetime.Scoped, binding);
        }

        public static void AddScoped<TDependency, TImplementation>([NotNull] this IDependencyCollection collection, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.AddDependency<TDependency, TImplementation>(Lifetime.Scoped, binding);
        }

        /// <summary>
        /// Adds the specified dependency as a scoped instance. Returns a <see cref="bool"/> specifying if the instance was added.
        /// </summary>
        /// <typeparam name="TDependency">Specifies the dependencies type.</typeparam>
        /// <param name="dependency">Specifies how to instantiate the dependency, including a provider to get existing dependencies.</param>
        /// <param name="binding">Specifies the binding of a dependency.</param>
        /// <exception cref="ArgumentNullException">Thrown when a null value is provided.</exception>
        /// <remarks>Only adds the dependency if no pre-existing instances are found.</remarks>
        public static bool TryAddScoped<TDependency>([NotNull] this IDependencyCollection collection, [NotNull] Func<TDependency> dependency, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.TryAddDependency(dependency, Lifetime.Scoped, binding);
        }

        /// <summary>
        /// Adds the specified dependency as a scoped instance. Returns a <see cref="bool"/> specifying if the instance was added.
        /// </summary>
        /// <typeparam name="TDependency">Specifies the dependencies type.</typeparam>
        /// <param name="dependency">Specifies the dependency whilst supplying a provided to get an existing dependency.</param>
        /// <param name="binding">Specifies the binding of a dependency.</param>
        /// <exception cref="ArgumentNullException">Thrown when a null value is provided.</exception>
        /// <remarks>Only adds the dependency if no pre-existing instances are found.</remarks>
        public static bool TryAddScoped<TDependency>([NotNull] this IDependencyCollection collection, [NotNull] Func<IDependencyProvider, TDependency> dependency, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.TryAddDependency(dependency, Lifetime.Scoped, binding);
        }

        public static bool TryAddScoped<TDependency, TImplementation>([NotNull] this IDependencyCollection collection, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.TryAddDependency<TDependency, TImplementation>(Lifetime.Scoped, binding);
        }

        /// <summary>
        /// Adds the specified dependency as a transient instance. Overrides existing instances.
        /// </summary>
        /// <typeparam name="TDependency">Specifies the dependencies type.</typeparam>
        /// <param name="dependency">Specifies how to instantiate the dependency, including a provider to get existing dependencies.</param>
        /// <param name="binding">Specifies the binding of a dependency.</param>
        /// <exception cref="ArgumentNullException">Thrown when a null value is provided.</exception>
        public static void AddTransient<TDependency>([NotNull] this IDependencyCollection collection, [NotNull] Func<TDependency> dependency, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.AddDependency(dependency, Lifetime.Transient, binding);
        }

        /// <summary>
        /// Adds the specified dependency as a transient instance. Overrides existing instances.
        /// </summary>
        /// <typeparam name="TDependency">Specifies the dependencies type.</typeparam>
        /// <param name="dependency">Specifies the dependency whilst supplying a provided to get an existing dependency.</param>
        /// <param name="binding">Specifies the binding of a dependency.</param>
        /// <exception cref="ArgumentNullException">Thrown when a null value is provided.</exception>
        public static void AddTransient<TDependency>([NotNull] this IDependencyCollection collection, [NotNull] Func<IDependencyProvider, TDependency> dependency, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.AddDependency(dependency, Lifetime.Transient, binding);
        }

        public static void AddTransient<TDependency, TImplementation>([NotNull] this IDependencyCollection collection, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.AddDependency<TDependency, TImplementation>(Lifetime.Transient, binding);
        }

        /// <summary>
        /// Adds the specified dependency as a transient instance. Returns a <see cref="bool"/> specifying if the instance was added.
        /// </summary>
        /// <typeparam name="TDependency">Specifies the dependencies type.</typeparam>
        /// <param name="dependency">Specifies how to instantiate the dependency, including a provider to get existing dependencies.</param>
        /// <param name="binding">Specifies the binding of a dependency.</param>
        /// <exception cref="ArgumentNullException">Thrown when a null value is provided.</exception>
        /// <remarks>Only adds the dependency if no pre-existing instances are found.</remarks>
        public static bool TryAddTransient<TDependency>([NotNull] this IDependencyCollection collection, [NotNull] Func<TDependency> dependency, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.TryAddDependency(dependency, Lifetime.Transient, binding);
        }

        /// <summary>
        /// Adds the specified dependency as a transient instance. Returns a <see cref="bool"/> specifying if the instance was added.
        /// </summary>
        /// <typeparam name="TDependency">Specifies the dependencies type.</typeparam>
        /// <param name="dependency">Specifies the dependency whilst supplying a provided to get an existing dependency.</param>
        /// <param name="binding">Specifies the binding of a dependency.</param>
        /// <exception cref="ArgumentNullException">Thrown when a null value is provided.</exception>
        /// <remarks>Only adds the dependency if no pre-existing instances are found.</remarks>
        public static bool TryAddTransient<TDependency>([NotNull] this IDependencyCollection collection, [NotNull] Func<IDependencyProvider, TDependency> dependency, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.TryAddDependency(dependency, Lifetime.Transient, binding);
        }

        public static bool TryAddTransient<TDependency, TImplementation>([NotNull] this IDependencyCollection collection, Binding binding = Binding.Bound)
        {
            if(collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.TryAddDependency<TDependency, TImplementation>(Lifetime.Transient, binding);
        }
    }
}
