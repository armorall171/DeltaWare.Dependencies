using DeltaWare.Dependencies.Abstractions;
using DeltaWare.Dependencies.Exceptions;
using DeltaWare.Dependencies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace DeltaWare.Dependencies
{
    public class DependencyProvider: IDependencyProvider
    {
        private readonly Dictionary<Type, IDependencyDescriptor> _dependencies;

        private readonly Dictionary<Type, IDependencyInstance> _singletonInstances;

        private readonly Dictionary<Type, IDependencyInstance> _scopedInstances = new Dictionary<Type, IDependencyInstance>();

        private readonly List<object> _disposableDependencies = new List<object>();

        /// <summary>
        /// Creates a new instance of the <see cref="IDependencyProvider"/>.
        /// </summary>
        /// <param name="dependencies">The collection of dependencies this provider has control over.</param>
        /// <param name="singletonInstances">The singleton instances the <see cref="DependencyCollection"/> has control over.</param>
        internal DependencyProvider(Dictionary<Type, IDependencyDescriptor> dependencies, Dictionary<Type, IDependencyInstance> singletonInstances)
        {
            _dependencies = dependencies;
            _singletonInstances = singletonInstances;
        }

        public TDependency GetDependency<TDependency>()
        {
            Type dependencyType = typeof(TDependency);

            if(_scopedInstances.TryGetValue(dependencyType, out IDependencyInstance dependency))
            {
                return (TDependency)dependency.Instance;
            }

            if(_singletonInstances.TryGetValue(dependencyType, out dependency))
            {
                return (TDependency)dependency.Instance;
            }

            return InstantiateDependency<TDependency>();
        }

        public List<TDependency> GetDependencies<TDependency>()
        {
            // Get all registered dependencies that inherit the specified type.
            IEnumerable<Type> dependencyTypes = _dependencies.Keys.Where(k => k.GetInterfaces().Contains(typeof(TDependency)));

            List<TDependency> dependencies = new List<TDependency>();

            foreach(Type dependencyType in dependencyTypes)
            {
                TDependency instantiatedDependency;

                if(_scopedInstances.TryGetValue(dependencyType, out IDependencyInstance dependency))
                {
                    // If the dependency has been instantiated it is retrieved.
                    instantiatedDependency = (TDependency)dependency.Instance;
                }
                else
                {
                    // If it has not been instantiated a new instance will be created.
                    instantiatedDependency = (TDependency)InstantiateDependency(dependencyType);
                }

                dependencies.Add(instantiatedDependency);
            }

            return dependencies;
        }

        public bool TryGetDependency<TDependency>(out TDependency dependencyInstance)
        {
            if(!HasDependency<TDependency>())
            {
                dependencyInstance = default;

                return false;
            }

            dependencyInstance = GetDependency<TDependency>();

            return true;
        }

        public bool HasDependency<TDependency>()
        {
            return _dependencies.ContainsKey(typeof(TDependency));
        }

        public TDependency InstantiateDependency<TDependency>()
        {
            return (TDependency)InstantiateDependency(typeof(TDependency));
        }

        public virtual object InstantiateDependency(Type dependencyType)
        {
            if(!_dependencies.TryGetValue(dependencyType, out IDependencyDescriptor dependency))
            {
                throw new DependencyNotFoundException(dependencyType);
            }

            IDependencyInstance dependencyInstance = dependency.GetInstance(this);

            // Singleton instances are not tracked so are returned immediately.
            if(dependencyInstance.Lifetime == Lifetime.Singleton)
            {
                _singletonInstances.Add(dependencyType, dependencyInstance);

                return dependencyInstance.Instance;
            }

            // Only scoped are tracked by the Provider so they are added.
            // Transient are not tracked at all, unless they are disposable.
            if(dependencyInstance.Lifetime == Lifetime.Scoped)
            {
                _scopedInstances.Add(dependencyType, dependencyInstance);
            }

            if(dependencyType.GetInterfaces().Contains(typeof(IDisposable)))
            {
                // Track all disposable dependencies.
                _disposableDependencies.Add(dependencyInstance);
            }

            return dependencyInstance;
        }

        #region IDisposable

        private volatile bool _disposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(_disposed)
            {
                return;
            }

            if(disposing)
            {
                foreach(IDisposable disposable in _disposableDependencies)
                {
                    disposable.Dispose();
                }
            }

            _disposed = true;
        }

        #endregion
    }
}
