using System.Linq;
using MessageHandler.Runtime;
using Xunit;

namespace unittests.DependencyInjection
{
    public class When_resolving_dependencies_with_inheritance
    {
        [Fact]
        public void Will_register_dependency_only_once()
        {
            var container = new Container();

            var b = new B();
            container.Register(() => b);

            var all = container.ResolveAll<IA>();

            Assert.Equal(1, all.Count());
        }

        public interface IA
        {
        }

        public abstract class A : IA
        {
        }

        public class B : A
        {
            
        }
    }
}