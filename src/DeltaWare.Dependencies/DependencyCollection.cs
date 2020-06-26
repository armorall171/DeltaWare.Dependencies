using System;
using System.Collections.Generic;
using System.Text;
using DeltaWare.Dependencies.Abstractions;

namespace DeltaWare.Dependencies
{
    public class DependencyCollection: IDependencyCollection
    {
        public object Clone()
        {
            throw new NotImplementedException();
        }

        public TDependency GetDependency<TDependency>()
        {
            throw new NotImplementedException();
        }

        public List<TDependency> GetDependencies<TDependency>()
        {
            throw new NotImplementedException();
        }

        public bool TryGetDependency<TDependency>(out TDependency dependencyInstance)
        {
            throw new NotImplementedException();
        }

        public bool HasDependency<TDependency>()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void AddDependency<TDependency>(TDependency dependency, Binding binding = Binding.Bound)
        {
            throw new NotImplementedException();
        }

        public bool TryAddDependency<TDependency>(TDependency dependency, Binding binding = Binding.Bound)
        {
            throw new NotImplementedException();
        }
    }
}
