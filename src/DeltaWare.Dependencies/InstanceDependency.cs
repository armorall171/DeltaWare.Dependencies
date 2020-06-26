using DeltaWare.Dependencies.Abstractions;
using System;

namespace DeltaWare.Dependencies
{
    public class InstanceDependency: IDependency, IDisposable
    {
        public Binding Binding { get; }

        public object Instance { get; }

        public Type Type { get; }

        public InstanceDependency(object instance, Binding binding = Binding.Bound)
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

        public object Clone()
        {
            return new InstanceDependency(Instance, Binding.Unbound);
        }

        #region IDisposable Implementation

        private volatile bool _disposed;

        public void Dispose()
        {
            if (Binding != Binding.Bound)
            {
                return;
            }

            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposing || _disposed)
            {
                return;
            }

            ((IDisposable)Instance).Dispose();

            _disposed = true;
        }

        #endregion
    }
}
