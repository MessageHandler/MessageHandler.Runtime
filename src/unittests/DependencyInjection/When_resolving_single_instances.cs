using MessageHandler.Runtime;
using Xunit;

namespace unittests.DependencyInjection
{
    public class When_resolving_single_instances
    {
        [Fact]
        public void Can_instantiate_any_class_with_default_constructor()
        {
            var container = new Container();
            var a = container.Resolve<A>();
            Assert.NotNull(a);
        }

        [Fact]
        public void Default_instantiate_per_call()
        {
            var container = new Container();

            var a1 = container.Resolve<A>();
            var a2 = container.Resolve<A>();

            Assert.NotStrictEqual(a1, a2);
        }

        [Fact]
        public void Can_instantiate_per_call()
        {
            var container = new Container();

            container.Register<A>(Lifecycle.InstancePerCall);

            var a1 = container.Resolve<A>();
            var a2 = container.Resolve<A>();

            Assert.NotStrictEqual(a1, a2);
        }

        [Fact]
        public void Can_instantiate_singleton()
        {
            var container = new Container();

            container.Register<A>(Lifecycle.Singleton);

            var a1 = container.Resolve<A>();
            var a2 = container.Resolve<A>();

            Assert.StrictEqual(a1, a2);
        }

        public class A { }
    }
}
