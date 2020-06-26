using DeltaWare.Dependencies.Abstractions;
using DeltaWare.Dependencies.Exceptions;
using System;
using System.Collections.Generic;

namespace DeltaWare.Dependencies
{
    public class DependencyProvider: IDependencyProvider
    {
        private readonly Dictionary<Type, IDependency> _dependencyTypeMap;

        private readonly Dictionary<Type, IDependency> _instantiatedDependencies = new Dictionary<Type, IDependency>();

        internal DependencyProvider(Dictionary<Type, IDependency> dependencyTypeMap)
        {
            _dependencyTypeMap = dependencyTypeMap;
        }

        public TDependency GetDependency<TDependency>()
        {
            Type dependencyType = typeof(TDependency);

            if(_instantiatedDependencies.TryGetValue(dependencyType, out IDependency dependency))
            {
                return (TDependency)dependency.Instance;
            }

            return InstantiateDependency<TDependency>();
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
            return _dependencyTypeMap.ContainsKey(typeof(TDependency));
        }

        public TDependency InstantiateDependency<TDependency>()
        {
            Type dependencyType = typeof(TDependency);

            if(!_dependencyTypeMap.TryGetValue(dependencyType, out IDependency dependency))
            {
                throw new DependencyNotFoundException(dependencyType);
            }

            TDependency instance = (TDependency)dependency.Instance;

            _instantiatedDependencies.Add(dependencyType, new Dependency(instance, dependency.Binding));

            return instance;
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
                foreach(IDependency dependency in _instantiatedDependencies.Values)
                {
                    if(dependency is IDisposable disposable)
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
