using MessageHandler.EventProcessing.Runtime;
using Xunit;

namespace unittests.DependencyInjection
{
    public class When_resolving_dependencies
    {
        [Fact]
        public void Can_instantiate_dependency()
        {
            var container = new Container();

            container.Register<A>();
            container.Register<B>();

            var b = container.Resolve<B>();

            Assert.NotNull(b.A);
        }

        public class A
        {
        }

        public class B
        {
            public A A { get; }

            public B(A a)
            {
                A = a;
            }
        }
    }
}
