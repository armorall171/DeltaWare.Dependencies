using DeltaWare.Dependencies.Abstractions;
using DeltaWare.Dependencies.Interfaces;
using DeltaWare.Dependencies.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DeltaWare.Dependencies
{
    /// <inheritdoc cref="IDependencyCollection"/>
    public class DependencyCollection: IDependencyCollection
    {
        private readonly Dictionary<Type, IDependencyDescriptor> _dependencies = new Dictionary<Type, IDependencyDescriptor>();

        private readonly Dictionary<Type, IDependencyInstance> _singletonInstances = new Dictionary<Type, IDependencyInstance>();

        /// <summary>
        /// Creates a new instance of <see cref="DependencyCollection"/>.
        /// </summary>
        public DependencyCollection()
        {
        }

        /// <inheritdoc cref="IDependencyCollection.AddDependency{TDependency}(Func{TDependency}, Lifetime, Binding)"/>
        public void AddDependency<TDependency>([NotNull] Func<TDependency> dependency, Lifetime lifetime, Binding binding = Binding.Bound) where TDependency : class
        {
            if(dependency == null)
            {
                throw new ArgumentNullException(nameof(dependency));
            }

            Type dependencyType = typeof(TDependency);

            IDependencyDescriptor dependencyDescriptor = new DependencyDescriptor<TDependency>(dependency, lifetime, binding);

            if(!_dependencies.TryAdd(dependencyType, dependencyDescriptor))
            {
                _dependencies[dependencyType] = dependencyDescriptor;
            }
        }

        /// <inheritdoc cref="IDependencyCollection.AddDependency{TDependency}(Func{IDependencyProvider, TDependency}, Lifetime, Binding)"/>
        public void AddDependency<TDependency>([NotNull] Func<IDependencyProvider, TDependency> dependency, Lifetime lifetime, Binding binding = Binding.Bound) where TDependency : class
        {
            if(dependency == null)
            {
                throw new ArgumentNullException(nameof(dependency));
            }

            Type dependencyType = typeof(TDependency);

            IDependencyDescriptor dependencyDescriptor = new DependencyDescriptor<TDependency>(dependency, lifetime, binding);

            if(!_dependencies.TryAdd(dependencyType, dependencyDescriptor))
            {
                _dependencies[dependencyType] = dependencyDescriptor;
            }
        }

        /// <inheritdoc cref="IDependencyCollection.TryAddDependency{TDependency}(Func{TDependency}, Lifetime, Binding)"/>
        public bool TryAddDependency<TDependency>([NotNull] Func<TDependency> dependency, Lifetime lifetime, Binding binding = Binding.Bound) where TDependency : class
        {
            if(dependency == null)
            {
                throw new ArgumentNullException(nameof(dependency));
            }

            Type dependencyType = typeof(TDependency);

            IDependencyDescriptor dependencyDescriptor = new DependencyDescriptor<TDependency>(dependency, lifetime, binding);

            return _dependencies.TryAdd(dependencyType, dependencyDescriptor);
        }

        /// <inheritdoc cref="IDependencyCollection.TryAddDependency{TDependency}(Func{IDependencyProvider, TDependency}, Lifetime, Binding)"/>
        public bool TryAddDependency<TDependency>([NotNull] Func<IDependencyProvider, TDependency> dependency, Lifetime lifetime, Binding binding = Binding.Bound) where TDependency : class
        {
            if(dependency == null)
            {
                throw new ArgumentNullException(nameof(dependency));
            }

            Type dependencyType = typeof(TDependency);

            IDependencyDescriptor dependencyDescriptor = new DependencyDescriptor<TDependency>(dependency, lifetime, binding);

            return _dependencies.TryAdd(dependencyType, dependencyDescriptor);
        }

        /// <inheritdoc cref="IDependencyCollection.HasDependency{TDependency}"/>
        public bool HasDependency<TDependency>()
        {
            return _dependencies.ContainsKey(typeof(TDependency));
        }

        /// <inheritdoc cref="IDependencyCollection.BuildProvider"/>
        public IDependencyProvider BuildProvider()
        {
            return new DependencyProvider(_dependencies, _singletonInstances);
        }

        #region IDisposable

        private volatile bool _disposed;

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all bound instances of singleton dependencies.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if(_disposed)
            {
                return;
            }

            if(disposing)
            {
                foreach(IDependencyInstance dependencyInstance in _singletonInstances.Values)
                {
                    if(dependencyInstance.Instance is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }

            _disposed = true;
        }

        #endregion
    }
}
