using DeltaWare.Dependencies.Abstractions;
using System;

namespace DeltaWare.Dependencies
{
    public class Dependency<TDependency>: IDependency<TDependency>
    {
        private readonly Func<TDependency> _builder;

        public Binding Binding { get; }

        public TDependency Instance => _builder.Invoke();

        public Type Type => typeof(TDependency);

        public Dependency(Func<TDependency> builder, Binding binding = Binding.Bound)
        {
            _builder = builder;
            Binding = binding;
        }

        public object Clone()
        {
            return new Dependency<TDependency>(_builder, Binding.Unbound);
        }
    }
}
