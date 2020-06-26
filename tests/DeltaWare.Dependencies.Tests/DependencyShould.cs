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
        public void GetInstanceInt()
        {
            IDependency dependency = new Dependency(26);

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

            disposable.IsDisposed.ShouldBe(false);

            dependency.Dispose();

            disposable.IsDisposed.ShouldBe(true);
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

            disposable.IsDisposed.ShouldBe(false);

            dependency.Dispose();

            disposable.IsDisposed.ShouldBe(false);
        }
    }
}
