using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageHandler.EventProcessing.Runtime;
using Xunit;

namespace unittests.DependencyInjection
{
    public class When_resolving_interface_dependencies
    {
        [Fact]
        public void Can_resolve_dependency_by_interface()
        {
            var container = new Container();

            container.Register<A>();
            container.Register<B>();

            var b = container.Resolve<B>();

            Assert.NotNull(b.A);
            Assert.IsType<A>(b.A);
        }

        public interface IA{}

        public class A : IA { }

        public class B
        {
            public IA A { get; }

            public B(IA a)
            {
                A = a;
            }
        }
    }
}
