using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DeltaWare.Dependencies
{
    public interface IDependencyProvider: IDisposable
    {
        /// <summary>
        /// Returns the <typeparamref name="TDependency"/> if it is not found an Exception will be thrown.
        /// </summary>
        /// <typeparam name="TDependency">The <typeparamref name="TDependency"/> to be returned.</typeparam>
        TDependency GetDependency<TDependency>();

        /// <summary>
        /// Returns all dependencies that inherit the specified type <typeparamref name="TDependency"/>.
        /// </summary>
        /// <typeparam name="TDependency">The type that the dependencies inherit.</typeparam>
        List<TDependency> GetDependencies<TDependency>();

        /// <summary>
        /// Returns a <see cref="bool"/> specifying if the dependency was found.
        /// </summary>
        /// <typeparam name="TDependency">The <typeparamref name="TDependency"/> to be returned.</typeparam>
        /// <param name="dependencyInstance">The <typeparamref name="TDependency"/> that was found.</param>
        bool TryGetDependency<TDependency>(out TDependency dependencyInstance);

        /// <summary>
        /// Returns a <see cref="bool"/> specifying if the <see cref="IDependencyProvider"/> contains the specified type.
        /// </summary>
        /// <typeparam name="TDependency"></typeparam>
        /// <returns></returns>
        bool HasDependency<TDependency>();
    }
}
