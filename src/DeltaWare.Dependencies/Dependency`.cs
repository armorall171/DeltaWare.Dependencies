using DeltaWare.Dependencies.Abstractions;
using System;

namespace DeltaWare.Dependencies
{
    public class Dependency<TDependency>: IDependency<TDependency>
    {
        private readonly Func<TDependency> _builder;

        public Binding Binding { get; }

        object IDependency.Instance => Instance;

        public TDependency Instance => _builder.Invoke();

        public Type Type => typeof(TDependency);

        public Dependency(Func<TDependency> builder, Binding binding = Binding.Bound)
        {
            _builder = builder;
            Binding = binding;


            if(typeof(TDependency).GetInterface(nameof(IDisposable)) != null)
            {
                Binding = binding;
            }
            else
            {
                Binding = Binding.Unbound;
            }
        }
    }
}
