using DeltaWare.Dependencies.Abstractions;
using System;

namespace DeltaWare.Dependencies.Interfaces
{
    public interface IDependencyDescriptor
    {
        /// <summary>
        /// Specifies the <see cref="Binding"/> of the <see cref="IDependencyDescriptor"/>.
        /// </summary>
        Binding Binding { get; }

        /// <summary>
        /// Specifies the <see cref="Lifetime"/> of the <see cref="IDependencyDescriptor"/>.
        /// </summary>
        Lifetime Lifetime { get; }

        /// <summary>
        /// Specified the <see cref="Type"/> of the <see cref="IDependencyDescriptor"/>,
        /// </summary>
        Type Type { get; }

        IDependencyInstance GetInstance(IDependencyProvider provider);
    }
}
