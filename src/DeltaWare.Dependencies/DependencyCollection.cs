using DeltaWare.Dependencies.Abstractions;
using System;
using System.Collections.Generic;

namespace DeltaWare.Dependencies
{
    public class DependencyCollection: IDependencyCollection
    {
        private readonly Dictionary<Type, IDependency> _dependencyTypeMap = new Dictionary<Type, IDependency>();

        public void AddDependency<TDependency>(Func<TDependency> builder, Binding binding = Binding.Bound)
        {
            Type dependencyType = typeof(TDependency);

            IDependency dependency = new Dependency<TDependency>(builder, binding);

            if(!_dependencyTypeMap.TryAdd(dependencyType, dependency))
            {
                _dependencyTypeMap[dependencyType] = dependency;
            }
        }

        public bool HasDependency<TDependency>()
        {
            return _dependencyTypeMap.ContainsKey(typeof(TDependency));
        }

        public bool TryAddDependency<TDependency>(Func<TDependency> builder, Binding binding = Binding.Bound)
        {
            Type dependencyType = typeof(TDependency);

            IDependency dependency = new Dependency<TDependency>(builder, binding);

            return _dependencyTypeMap.TryAdd(dependencyType, dependency);
        }

        public IDependencyProvider BuildProvider()
        {
            return new DependencyProvider(_dependencyTypeMap);
        }
    }
}
