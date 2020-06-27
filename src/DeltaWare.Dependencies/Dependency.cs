using DeltaWare.Dependencies.Abstractions;
using System;

namespace DeltaWare.Dependencies
{
    public class Dependency: IDependency, IDisposable
    {
        public Binding Binding { get; }

        public object Instance { get; }

        public Type Type { get; }

        public Dependency(object instance, Binding binding = Binding.Bound)
        {
            Type = instance.GetType();
            Instance = instance;

            if(instance is IDisposable)
            {
                Binding = binding;
            }
            else
            {
                Binding = Binding.Unbound;
            }
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
