using DeltaWare.Dependencies.Abstractions;
using DeltaWare.Dependencies.Interfaces;
using DeltaWare.Dependencies.Types;
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DeltaWare.Dependencies
{
    public class DependencyCollection: IDependencyCollection
    {
        private readonly Dictionary<Type, IDependencyDescriptor> _dependencies = new Dictionary<Type, IDependencyDescriptor>();

        private readonly Dictionary<Type, IDependencyInstance> _singletonInstances = new Dictionary<Type, IDependencyInstance>();

        public void AddDependency<TDependency>(Func<TDependency> builder, Lifetime lifetime, Binding binding = Binding.Bound)
        {
            Type dependencyType = typeof(TDependency);

            IDependencyDescriptor dependency = new DependencyDescriptor<TDependency>(builder, lifetime, binding);

            if(!_dependencies.TryAdd(dependencyType, dependency))
            {
                _dependencies[dependencyType] = dependency;
            }
        }

        public void AddDependency<TDependency>(Func<IDependencyProvider, TDependency> builder, Lifetime lifetime, Binding binding = Binding.Bound)
        {
            Type dependencyType = typeof(TDependency);

            IDependencyDescriptor dependency = new DependencyDescriptor<TDependency>(builder, lifetime, binding);

            if(!_dependencies.TryAdd(dependencyType, dependency))
            {
                _dependencies[dependencyType] = dependency;
            }
        }

        public bool TryAddDependency<TDependency>(Func<TDependency> builder, Lifetime lifetime, Binding binding = Binding.Bound)
        {
            Type dependencyType = typeof(TDependency);

            IDependencyDescriptor dependency = new DependencyDescriptor<TDependency>(builder, lifetime, binding);

            return _dependencies.TryAdd(dependencyType, dependency);
        }

        public bool TryAddDependency<TDependency>(Func<IDependencyProvider, TDependency> builder, Lifetime lifetime, Binding binding = Binding.Bound)
        {
            Type dependencyType = typeof(TDependency);

            IDependencyDescriptor dependency = new DependencyDescriptor<TDependency>(builder, lifetime, binding);

            return _dependencies.TryAdd(dependencyType, dependency);
        }

        public bool HasDependency<TDependency>()
        {
            return _dependencies.ContainsKey(typeof(TDependency));
        }

        public IDependencyProvider BuildProvider()
        {
            return new DependencyProvider(_dependencies, _singletonInstances);
        }

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
    }
}
