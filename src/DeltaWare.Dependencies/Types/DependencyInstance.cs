using DeltaWare.Dependencies.Abstractions;
using DeltaWare.Dependencies.Interfaces;
using System;

namespace DeltaWare.Dependencies.Types
{
    internal class DependencyInstance: IDependencyInstance
    {
        public Binding Binding { get; }

        public Lifetime Lifetime { get; }

        public Type Type { get; }

        public object Instance { get; }

        internal DependencyInstance(object instance, Type type, Lifetime lifetime, Binding binding)
        {
            Type = type;
            Instance = instance;
            Binding = binding;
            Lifetime = lifetime;
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

            if(disposing && Binding == Binding.Bound && Instance is IDisposable disposableImplementation)
            {
                disposableImplementation.Dispose();
            }

            _disposed = true;
        }

        #endregion
    }
}
