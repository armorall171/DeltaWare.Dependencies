using DeltaWare.Dependencies.Abstractions;
using DeltaWare.Dependencies.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<TDependency> GetDependencies<TDependency>()
        {
            // Get all registered dependencies that inherit the specified type.
            IEnumerable<Type> dependencyTypes = _dependencyTypeMap.Keys.Where(k => k.GetInterfaces().Contains(typeof(TDependency)));

            List<TDependency> dependencies = new List<TDependency>();

            foreach(Type dependencyType in dependencyTypes)
            {
                TDependency instantiatedDependency;

                if(_instantiatedDependencies.TryGetValue(dependencyType, out IDependency dependency))
                {
                    // If the dependency has been instantiated we retrieve the instance.
                    instantiatedDependency = (TDependency)dependency.Instance;
                }
                else
                {
                    // If it has not been instantiated we create a new instance.
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
            return _dependencyTypeMap.ContainsKey(typeof(TDependency));
        }

        public TDependency InstantiateDependency<TDependency>()
        {
            return (TDependency)InstantiateDependency(typeof(TDependency));
        }

        public object InstantiateDependency(Type dependencyType)
        {
            if(!_dependencyTypeMap.TryGetValue(dependencyType, out IDependency dependency))
            {
                throw new DependencyNotFoundException(dependencyType);
            }

            object instance = dependency.Instance;

            _instantiatedDependencies.Add(dependencyType, new Dependency(instance, dependencyType, dependency.Binding));

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
