﻿using DeltaWare.Dependencies.Abstractions;
using System;

namespace DeltaWare.Dependencies.Interfaces
{
    /// <summary>
    /// Represents an instance of a dependency.
    /// </summary>
    public interface IDependencyInstance: IDisposable
    {
        /// <summary>
        /// Specifies the binding of the dependency.
        /// </summary>
        Binding Binding { get; }

        /// <summary>
        /// Specifies the lifetime of the dependency.
        /// </summary>
        Lifetime Lifetime { get; }

        /// <summary>
        /// Specified the type of the dependency.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// The instance of the dependency.
        /// </summary>
        object Instance { get; }
    }
}
