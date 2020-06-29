﻿using System;
using System.Collections.Generic;

namespace DeltaWare.Dependencies.Abstractions
{
    public interface IDependencyProvider: IDisposable
    {
        /// <summary>
        /// Returns the <see cref="TDependency"/> if it is not found an Exception will be thrown.
        /// </summary>
        /// <typeparam name="TDependency">The <see cref="TDependency"/> to be returned.</typeparam>
        TDependency GetDependency<TDependency>();

        List<TDependency> GetDependencies<TDependency>();

        /// <summary>
        /// Returns a <see cref="bool"/> specifying if the <see cref="TDependency"/> was found.
        /// </summary>
        /// <typeparam name="TDependency">The <see cref="TDependency"/> to be returned.</typeparam>
        /// <param name="dependencyInstance">The <see cref="TDependency"/> that was found.</param>
        /// <returns></returns>
        bool TryGetDependency<TDependency>(out TDependency dependencyInstance);

        /// <summary>
        /// Returns a <see cref="bool"/> specifying if the <see cref="IDependencyProvider"/> contains the specified type.
        /// </summary>
        /// <typeparam name="TDependency"></typeparam>
        /// <returns></returns>
        bool HasDependency<TDependency>();
    }
}
