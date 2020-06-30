using DeltaWare.Dependencies.Abstractions;
using System;

namespace DeltaWare.Dependencies.Interfaces
{
    public interface IDependencyInstance: IDisposable
    {
        /// <summary>
        /// Specifies the <see cref="Binding"/> of the <see cref="IDependencyInstance"/>.
        /// </summary>
        Binding Binding { get; }

        /// <summary>
        /// Specifies the <see cref="Lifetime"/> of the <see cref="IDependencyInstance"/>.
        /// </summary>
        Lifetime Lifetime { get; }

        /// <summary>
        /// Specified the <see cref="Type"/> of the <see cref="IDependencyInstance"/>,
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// The current instance of the <see cref="IDependencyInstance"/>.
        /// </summary>
        object Instance { get; }
    }
}
