using System.CodeDom;
using System.Linq;
using MessageHandler.EventProcessing.Runtime;
using Xunit;

namespace unittests.DependencyInjection
{
    public class When_resolving_mutiple_interface
    {
        [Fact]
        void Can_resolve_all_classes_implementing_interface()
        {
            var container = new Container();

            container.Register<A1>();
            container.Register<A2>();

           var instances = container.ResolveAll<IA>();

            Assert.True(instances.Count() == 2);
        }

        public interface IA { }

        public class A1 : IA { }
        public class A2 : IA { }

    }
}
