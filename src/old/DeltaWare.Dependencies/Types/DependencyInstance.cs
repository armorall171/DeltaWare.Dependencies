﻿using DeltaWare.Dependencies.Abstractions;
using DeltaWare.Dependencies.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DeltaWare.Dependencies.Types
{
    /// <inheritdoc cref="IDependencyInstance"/>
    public class DependencyInstance: IDependencyInstance
    {
        /// <inheritdoc cref="IDependencyInstance.Binding"/>
        public Binding Binding { get; }

        /// <inheritdoc cref="IDependencyInstance.Lifetime"/>
        public Lifetime Lifetime { get; }

        /// <inheritdoc cref="IDependencyInstance.Type"/>
        public Type Type { get; }

        /// <inheritdoc cref="IDependencyInstance.Instance"/>
        public object Instance { get; }

        /// <summary>
        /// Creates a new instance of <see cref="DependencyInstance"/>.
        /// </summary>
        /// <param name="instance">The instance of the dependency.</param>
        /// <param name="type">Specifies the type of the dependency.</param>
        /// <param name="lifetime">Specifies the lifetime of the dependency.</param>
        /// <param name="binding">Specifies the binding on the dependency.</param>
        public DependencyInstance([NotNull] object instance, [NotNull] Type type, Lifetime lifetime, Binding binding)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));
            Binding = binding;
            Lifetime = lifetime;
        }

        #region IDisposable

        private volatile bool _disposed;

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            if(Binding != Binding.Bound)
            {
                return;
            }

            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the current instance of the dependency.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if(_disposed)
            {
                return;
            }

            if(disposing && Instance is IDisposable disposableImplementation)
            {
                disposableImplementation.Dispose();
            }

            _disposed = true;
        }

        #endregion
    }
}
