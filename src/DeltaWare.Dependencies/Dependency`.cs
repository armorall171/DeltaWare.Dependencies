using DeltaWare.Dependencies.Abstractions;
using System;

namespace DeltaWare.Dependencies
{
    public class Dependency<TDependency>: Dependency, IDependency<TDependency>
    {
        private readonly Func<TDependency> _builder;

        public new TDependency Instance => _builder.Invoke();

        public new Type Type => typeof(TDependency);

        public Dependency(Func<TDependency> builder, Binding binding = Binding.Bound) : base(binding)
        {
            _builder = builder;
        }
    }
}
