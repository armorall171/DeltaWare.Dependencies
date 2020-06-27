using DeltaWare.Dependencies.Abstractions;
using Shouldly;
using Xunit;

namespace DeltaWare.Dependencies.Tests
{
    public class DependencyShould
    {
        [Fact]
        public void GetInstanceString()
        {
            IDependency dependency = new Dependency("Hello");

            dependency.Instance.ShouldBe("Hello");
            dependency.Binding.ShouldBe(Binding.Unbound);
            dependency.Type.ShouldBe(typeof(string));
        }

        [Fact]
        public void GetInstanceStringGeneric()
        {
            IDependency dependency = new Dependency<string>(() => "Hello");

            dependency.Instance.ShouldBe("Hello");
            dependency.Binding.ShouldBe(Binding.Unbound);
            dependency.Type.ShouldBe(typeof(string));
        }

        [Fact]
        public void GetInstanceInt()
        {
            IDependency dependency = new Dependency(26);

            dependency.Instance.ShouldBe(26);
            dependency.Binding.ShouldBe(Binding.Unbound);
            dependency.Type.ShouldBe(typeof(int));
        }


        [Fact]
        public void GetInstanceIntGeneric()
        {
            IDependency dependency = new Dependency<int>(() => 26);

            dependency.Instance.ShouldBe(26);
            dependency.Binding.ShouldBe(Binding.Unbound);
            dependency.Type.ShouldBe(typeof(int));
        }

        [Fact]
        public void GetInstanceDisposableBound()
        {
            TestDisposable disposable = new TestDisposable
            {
                IntValue = 26,
                StringValue = "Hello"
            };

            Dependency dependency = new Dependency(disposable);

            dependency.Instance.ShouldBe(disposable);
            dependency.Binding.ShouldBe(Binding.Bound);
            dependency.Type.ShouldBe(typeof(TestDisposable));

            disposable.IsDisposed.ShouldBeFalse();

            dependency.Dispose();

            disposable.IsDisposed.ShouldBeTrue();
        }

        [Fact]
        public void GetInstanceDisposableBoundGeneric()
        {
            TestDisposable disposable = new TestDisposable
            {
                IntValue = 26,
                StringValue = "Hello"
            };

            Dependency<TestDisposable> dependency = new Dependency<TestDisposable>(() => disposable);

            dependency.Instance.ShouldBe(disposable);
            dependency.Binding.ShouldBe(Binding.Bound);
            dependency.Type.ShouldBe(typeof(TestDisposable));

            disposable.IsDisposed.ShouldBeFalse();
        }

        [Fact]
        public void GetInstanceDisposableUnbound()
        {
            TestDisposable disposable = new TestDisposable
            {
                IntValue = 26,
                StringValue = "Hello"
            };

            Dependency dependency = new Dependency(disposable, Binding.Unbound);

            dependency.Instance.ShouldBe(disposable);
            dependency.Binding.ShouldBe(Binding.Unbound);
            dependency.Type.ShouldBe(typeof(TestDisposable));

            disposable.IsDisposed.ShouldBeFalse();

            dependency.Dispose();

            disposable.IsDisposed.ShouldBeFalse();
        }

        [Fact]
        public void GetInstanceDisposableUnboundGeneric()
        {
            TestDisposable disposable = new TestDisposable
            {
                IntValue = 26,
                StringValue = "Hello"
            };

            Dependency<TestDisposable> dependency = new Dependency<TestDisposable>(() => disposable, Binding.Unbound);

            dependency.Instance.ShouldBe(disposable);
            dependency.Binding.ShouldBe(Binding.Unbound);
            dependency.Type.ShouldBe(typeof(TestDisposable));

            disposable.IsDisposed.ShouldBeFalse();
        }
    }
}
