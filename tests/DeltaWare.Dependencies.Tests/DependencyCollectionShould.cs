using DeltaWare.Dependencies.Abstractions;
using DeltaWare.Dependencies.Exceptions;
using Shouldly;
using Xunit;

namespace DeltaWare.Dependencies.Tests
{
    public class DependencyCollectionShould
    {
        [Fact]
        public void AddDependencyString()
        {
            IDependencyCollection dependencies = new DependencyCollection();

            dependencies.AddDependency(() => "Hello");

            using IDependencyProvider provider = dependencies.BuildProvider();

            provider.HasDependency<string>().ShouldBeTrue();
            provider.GetDependency<string>().ShouldBe("Hello");
        }

        [Fact]
        public void AddDependencyStringOverride()
        {
            IDependencyCollection dependencies = new DependencyCollection();

            dependencies.AddDependency(() => "Hello");

            dependencies.AddDependency(() => "New Hello");

            using IDependencyProvider provider = dependencies.BuildProvider();

            provider.HasDependency<string>().ShouldBeTrue();
            provider.GetDependency<string>().ShouldBe("New Hello");
        }

        [Fact]
        public void AddDependencyStringTryAdd()
        {
            IDependencyCollection dependencies = new DependencyCollection();

            dependencies.TryAddDependency(() => "Hello").ShouldBeTrue();
            dependencies.TryAddDependency(() => "New Hello").ShouldBeFalse();

            using IDependencyProvider provider = dependencies.BuildProvider();

            provider.HasDependency<string>().ShouldBeTrue();
            provider.GetDependency<string>().ShouldBe("Hello");
        }

        [Fact]
        public void AddDependencyInt()
        {
            IDependencyCollection dependencies = new DependencyCollection();

            dependencies.AddDependency(() => 34);

            using IDependencyProvider provider = dependencies.BuildProvider();

            provider.HasDependency<int>().ShouldBeTrue();
            provider.GetDependency<int>().ShouldBe(34);
        }

        [Fact]
        public void AddDependencyIntOverride()
        {
            IDependencyCollection dependencies = new DependencyCollection();

            dependencies.AddDependency(() => 34);
            dependencies.AddDependency(() => 42);

            using IDependencyProvider provider = dependencies.BuildProvider();

            provider.HasDependency<int>().ShouldBeTrue();
            provider.GetDependency<int>().ShouldBe(42);
        }

        [Fact]
        public void AddDependencyIntTryAdd()
        {
            IDependencyCollection dependencies = new DependencyCollection();

            dependencies.TryAddDependency(() => 34).ShouldBeTrue();
            dependencies.TryAddDependency(() => 42).ShouldBeFalse();

            using IDependencyProvider provider = dependencies.BuildProvider();

            provider.HasDependency<int>().ShouldBeTrue();
            provider.GetDependency<int>().ShouldBe(34);
        }

        [Fact]
        public void AddDependencyDisposableBound()
        {
            IDependencyCollection dependencies = new DependencyCollection();

            dependencies.AddDependency(() => new TestDisposable
            {
                IntValue = 34,
                StringValue = "Hello"
            });

            TestDisposable disposable;

            using(IDependencyProvider provider = dependencies.BuildProvider())
            {
                disposable = provider.GetDependency<TestDisposable>();

                disposable.IntValue.ShouldBe(34);
                disposable.StringValue.ShouldBe("Hello");
                disposable.IsDisposed.ShouldBeFalse();

                provider.TryGetDependency(out disposable).ShouldBeTrue();

                disposable.IntValue.ShouldBe(34);
                disposable.StringValue.ShouldBe("Hello");
                disposable.IsDisposed.ShouldBeFalse();
            }

            disposable.IsDisposed.ShouldBeTrue();

            // repeat the test as a new instance should be generated.
            using(IDependencyProvider provider = dependencies.BuildProvider())
            {
                disposable = provider.GetDependency<TestDisposable>();

                disposable.IntValue.ShouldBe(34);
                disposable.StringValue.ShouldBe("Hello");
                disposable.IsDisposed.ShouldBeFalse();

                provider.TryGetDependency(out disposable).ShouldBeTrue();

                disposable.IntValue.ShouldBe(34);
                disposable.StringValue.ShouldBe("Hello");
                disposable.IsDisposed.ShouldBeFalse();
            }

            disposable.IsDisposed.ShouldBeTrue();
        }

        [Fact]
        public void AddDependencyDisposableUnbound()
        {
            IDependencyCollection dependencies = new DependencyCollection();

            dependencies.AddDependency(() => new TestDisposable
            {
                IntValue = 34,
                StringValue = "Hello"
            }, Binding.Unbound);

            TestDisposable disposable;

            using(IDependencyProvider provider = dependencies.BuildProvider())
            {
                disposable = provider.GetDependency<TestDisposable>();

                disposable.IntValue.ShouldBe(34);
                disposable.StringValue.ShouldBe("Hello");
                disposable.IsDisposed.ShouldBeFalse();

                provider.TryGetDependency(out disposable).ShouldBeTrue();

                disposable.IntValue.ShouldBe(34);
                disposable.StringValue.ShouldBe("Hello");
                disposable.IsDisposed.ShouldBeFalse();
            }

            disposable.IsDisposed.ShouldBeFalse();

            disposable.Dispose();

            disposable.IsDisposed.ShouldBeTrue();
        }

        [Fact]
        public void AddDependencyInstanceDisposableBound()
        {
            IDependencyCollection dependencies = new DependencyCollection();

            TestDisposable testDisposable = new TestDisposable
            {
                IntValue = 34,
                StringValue = "Hello"
            };

            dependencies.AddDependency(() => testDisposable);

            TestDisposable disposable;

            using(IDependencyProvider provider = dependencies.BuildProvider())
            {
                disposable = provider.GetDependency<TestDisposable>();

                disposable.IntValue.ShouldBe(34);
                disposable.StringValue.ShouldBe("Hello");
                disposable.IsDisposed.ShouldBeFalse();

                provider.TryGetDependency(out disposable).ShouldBeTrue();

                disposable.IntValue.ShouldBe(34);
                disposable.StringValue.ShouldBe("Hello");
                disposable.IsDisposed.ShouldBeFalse();
            }

            disposable.IsDisposed.ShouldBeTrue();

            testDisposable.IsDisposed.ShouldBeTrue();

            // repeat the test, but it should still be disposed of.
            using(IDependencyProvider provider = dependencies.BuildProvider())
            {
                disposable = provider.GetDependency<TestDisposable>();
                disposable.IsDisposed.ShouldBeTrue();

                provider.TryGetDependency(out disposable).ShouldBeTrue();

                disposable.IsDisposed.ShouldBeTrue();
            }
        }

        [Fact]
        public void AddDependencyInstanceDisposableUnbound()
        {
            IDependencyCollection dependencies = new DependencyCollection();

            TestDisposable testDisposable = new TestDisposable
            {
                IntValue = 34,
                StringValue = "Hello"
            };

            dependencies.AddDependency(() => testDisposable, Binding.Unbound);

            TestDisposable disposable;

            using(IDependencyProvider provider = dependencies.BuildProvider())
            {
                disposable = provider.GetDependency<TestDisposable>();

                disposable.IntValue.ShouldBe(34);
                disposable.StringValue.ShouldBe("Hello");
                disposable.IsDisposed.ShouldBeFalse();
            }

            disposable.IsDisposed.ShouldBeFalse();

            disposable.Dispose();

            disposable.IsDisposed.ShouldBeTrue();
            testDisposable.IsDisposed.ShouldBeTrue();
        }

        [Fact]
        public void GetDependencyException()
        {
            IDependencyCollection dependencies = new DependencyCollection();

            using IDependencyProvider provider = dependencies.BuildProvider();

            Should.Throw<DependencyNotFoundException>(() => provider.GetDependency<TestDisposable>());
        }

        [Fact]
        public void TryGetDependencyNoResult()
        {
            IDependencyCollection dependencies = new DependencyCollection();

            using IDependencyProvider provider = dependencies.BuildProvider();

            bool result = provider.TryGetDependency(out TestDisposable disposable);

            result.ShouldBeFalse();
            disposable.ShouldBeNull();
        }
    }
}
