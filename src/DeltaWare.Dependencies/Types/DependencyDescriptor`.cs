using DeltaWare.Dependencies.Abstractions;
using DeltaWare.Dependencies.Interfaces;
using System;

namespace DeltaWare.Dependencies.Types
{
    internal class DependencyDescriptor<TDependency>: IDependencyDescriptor
    {
        private readonly Func<TDependency> _builder = null;

        private readonly Func<IDependencyProvider, TDependency> _providerBuilder = null;

        public Binding Binding { get; }

        public Lifetime Lifetime { get; }

        public Type Type => typeof(TDependency);

        public DependencyDescriptor(Func<TDependency> builder, Lifetime lifetime, Binding binding)
        {
            _builder = builder;
            Binding = binding;
            Lifetime = lifetime;
        }

        public DependencyDescriptor(Func<IDependencyProvider, TDependency> builder, Lifetime lifetime, Binding binding)
        {
            _providerBuilder = builder;
            Binding = binding;
            Lifetime = lifetime;
        }

        public IDependencyInstance GetInstance(IDependencyProvider provider)
        {
            IDependencyInstance instance;

            if(_builder != null)
            {
                instance = new DependencyInstance(_builder.Invoke(), Type, Lifetime, Binding);
            }
            else if(_providerBuilder != null)
            {
                instance = new DependencyInstance(_providerBuilder.Invoke(provider), Type, Lifetime, Binding);
            }
            else
            {
                throw new ArgumentNullException();
            }

            return instance;
        }
    }
}
