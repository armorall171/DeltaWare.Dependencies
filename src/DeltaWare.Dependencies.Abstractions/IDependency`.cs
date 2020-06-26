namespace DeltaWare.Dependencies.Abstractions
{
    public interface IDependency<out TDependency>
    {
        /// <summary>
        /// The current instance of the <see cref="TDependency"/>.
        /// </summary>
        new TDependency Instance { get; }
    }
}
