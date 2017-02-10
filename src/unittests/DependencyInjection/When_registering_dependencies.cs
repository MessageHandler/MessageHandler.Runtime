using MessageHandler.Runtime;
using Xunit;

namespace unittests.DependencyInjection
{
    public class When_registering_dependencies
    {
        [Fact]
        public void Can_register_by_generic()
        {
            var container = new Container();
            container.Register<A>();
            Assert.NotNull(container.Resolve<A>());
        }

        [Fact]
        public void Can_register_by_generic_per_call()
        {
            var container = new Container();
            container.Register<A>(Lifecycle.InstancePerCall);
            var a1 = container.Resolve<A>();
            var a2 = container.Resolve<A>();
            Assert.NotStrictEqual(a1, a2);
        }

        [Fact]
        public void Can_register_by_generic_singleton()
        {
            var container = new Container();
            container.Register<A>(Lifecycle.Singleton);
            var a1 = container.Resolve<A>();
            var a2 = container.Resolve<A>();
            Assert.StrictEqual(a1, a2);
        }

        [Fact]
        public void Can_register_by_func()
        {
            var container = new Container();
            container.Register(() => new A());
            Assert.NotNull(container.Resolve<A>());
        }

        [Fact]
        public void Can_register_by_func_per_call()
        {
            var container = new Container();
            container.Register(Lifecycle.InstancePerCall, () => new A());
            var a1 = container.Resolve<A>();
            var a2 = container.Resolve<A>();
            Assert.NotStrictEqual(a1, a2);
        }

        [Fact]
        public void Can_register_by_func_singleton()
        {
            var container = new Container();
            container.Register(Lifecycle.Singleton, () => new A());
            var a1 = container.Resolve<A>();
            var a2 = container.Resolve<A>();
            Assert.StrictEqual(a1, a2);
        }

        [Fact]
        public void Can_register_by_type()
        {
            var container = new Container();
            container.Register(typeof(A));
            Assert.NotNull(container.Resolve<A>());
        }

        [Fact]
        public void Can_register_by_type_per_call()
        {
            var container = new Container();
            container.Register(typeof(A), Lifecycle.InstancePerCall);
            var a1 = container.Resolve<A>();
            var a2 = container.Resolve<A>();
            Assert.NotStrictEqual(a1, a2);
        }

        [Fact]
        public void Can_register_by_type_singleton()
        {
            var container = new Container();
            container.Register(typeof(A), Lifecycle.Singleton);
            var a1 = container.Resolve<A>();
            var a2 = container.Resolve<A>();
            Assert.StrictEqual(a1, a2);
        }

        [Fact]
        public void Can_register_by_type_func()
        {
            var container = new Container();
            container.Register(typeof(A), () => new A());
            Assert.NotNull(container.Resolve<A>());
        }

        [Fact]
        public void Can_register_by_type_func_per_call()
        {
            var container = new Container();
            container.Register(typeof(A), Lifecycle.InstancePerCall, () => new A());
            var a1 = container.Resolve<A>();
            var a2 = container.Resolve<A>();
            Assert.NotStrictEqual(a1, a2);
        }

        [Fact]
        public void Can_register_by_type_func_singleton()
        {
            var container = new Container();
            container.Register(typeof(A), Lifecycle.Singleton, () => new A());
            var a1 = container.Resolve<A>();
            var a2 = container.Resolve<A>();
            Assert.StrictEqual(a1, a2);
        }

        public class A { }

    }
}