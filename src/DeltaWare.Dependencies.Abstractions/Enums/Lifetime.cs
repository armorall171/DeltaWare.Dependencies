// ReSharper disable once CheckNamespace
namespace DeltaWare.Dependencies.Abstractions
{
    /// <summary>
    /// Specifies the lifetime of an <see cref="IDependencyInstance"/>.
    /// </summary>
    public enum Lifetime
    {
        /// <summary>
        /// Specifies that a single instance of the <see cref="IDependencyInstance"/> will be created for the lifetime of the <see cref="IDependencyCollection"/>.
        /// </summary>
        Singleton,
        /// <summary>
        /// Specifies that a new instance of the <see cref="IDependencyInstance"/> will be created for each instance of the <see cref="IDependencyProvider"/>.
        /// </summary>
        Scoped,
        /// <summary>
        /// Specifies that a new instance of the <see cref="IDependencyInstance"/> will be created every time it is requested.
        /// </summary>
        Transient
    }
}
