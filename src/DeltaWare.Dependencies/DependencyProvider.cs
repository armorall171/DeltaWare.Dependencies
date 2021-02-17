using DeltaWare.Dependencies.Abstractions;
using DeltaWare.Dependencies.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DeltaWare.Dependencies
{
    /// <inheritdoc cref="IDependencyProvider"/>
    internal class DependencyProvider: IDependencyProvider
    {
        private readonly DependencyCollection _sourceCollection;

        private readonly Dictionary<Type, IDependencyInstance> _scopedInstances = new Dictionary<Type, IDependencyInstance>();

        private readonly List<object> _disposableDependencies = new List<object>();

        private readonly object _scopeLock = new object();

        public DependencyProvider([NotNull] DependencyCollection sourceCollection)
        {
            _sourceCollection = sourceCollection ?? throw new ArgumentNullException(nameof(sourceCollection));
        }

        /// <inheritdoc cref="IDependencyProvider.GetDependency{TDependency}"/>
        public TDependency GetDependency<TDependency>()
        {
            IDependencyDescriptor descriptor = _sourceCollection.GetDependencyDescriptor<TDependency>();

            return (TDependency)GetDependency(descriptor);
        }

        public object GetDependency(Type dependencyType)
        {
            IDependencyDescriptor descriptor = _sourceCollection.GetDependencyDescriptor(dependencyType);

            return GetDependency(descriptor);
        }

        /// <inheritdoc cref="IDependencyProvider.GetDependencies{TDependency}"/>
        public List<TDependency> GetDependencies<TDependency>()
        {
            // Get all registered dependencies that inherit the specified type.
            List<TDependency> dependencies = new List<TDependency>();

            foreach(IDependencyDescriptor descriptor in _sourceCollection.GetDependencyDescriptors<TDependency>())
            {
                dependencies.Add((TDependency)GetDependency(descriptor));
            }

            return dependencies;
        }

        public object GetDependency(IDependencyDescriptor descriptor)
        {
            if(descriptor.Lifetime == Lifetime.Singleton)
            {
                return _sourceCollection.GetSingletonInstance(descriptor, this).Instance;
            }

            lock(_scopeLock)
            {
                if(_scopedInstances.TryGetValue(descriptor.Type, out IDependencyInstance instance))
                {
                    return instance.Instance;
                }

                instance = descriptor.GetInstance(this);

                // Only scoped are tracked by the Provider so they are added.
                // Transient are not tracked at all, unless they are disposable.
                if(instance.Lifetime == Lifetime.Scoped)
                {
                    _scopedInstances.Add(descriptor.Type, instance);
                }

                if(instance.IsDisposable)
                {
                    // Track all disposable dependencies.
                    _disposableDependencies.Add(instance);
                }

                return instance.Instance;
            }
        }

        /// <inheritdoc cref="IDependencyProvider.TryGetDependency{TDependency}"/>
        public bool TryGetDependency<TDependency>(out TDependency dependencyInstance)
        {
            if(HasDependency<TDependency>())
            {
                dependencyInstance = GetDependency<TDependency>();

                return true;
            }

            dependencyInstance = default;

            return false;
        }

        public bool TryGetDependency(Type dependencyType, out object dependencyInstance)
        {
            if (HasDependency(dependencyType))
            {
                dependencyInstance = GetDependency(dependencyType);

                return true;
            }

            dependencyInstance = default;
            
            return false;
        }

        /// <inheritdoc cref="IDependencyProvider.HasDependency{TDependency}"/>
        public bool HasDependency<TDependency>()
        {
            return _sourceCollection.HasDependency<TDependency>();
        }
        
        public bool HasDependency(Type dependencyType)
        {
            return _sourceCollection.HasDependency(dependencyType);
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
        /// Disposes all bound instances of scoped dependencies.
        /// </summary>
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
