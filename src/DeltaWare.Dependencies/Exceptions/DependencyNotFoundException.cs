using System;

namespace DeltaWare.Dependencies.Exceptions
{
    public class DependencyNotFoundException: Exception
    {
        public DependencyNotFoundException(Type type) : base($"The specified dependency of type {type.Name} could not be found")
        {
        }
    }
}
