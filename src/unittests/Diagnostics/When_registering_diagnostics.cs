using System.Configuration;
using System.Threading.Tasks;
using MessageHandler.Runtime;
using MessageHandler.Runtime.ConfigurationSettings;
using MessageHandler.Runtime.Diagnostics;
using Xunit;

namespace unittests.Diagnostics
{
    public class When_registering_diagnostics
    {
        [Fact]
        public void Can_register_diagnostic_and_find_it_in_the_container()
        {
            var myDiagnostic = new MyDiagnostic();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterDiagnosticSink(myDiagnostic);
            Assert.NotNull(container.Resolve<IDiagnosticSink>());
            Assert.IsType<MyDiagnostic>(container.Resolve<IDiagnosticSink>());
        }

        [Fact]
        public void Can_register_diagnostic_instance_and_find_its_type_in_the_settings()
        {
            var settings = new Settings();
            var myDiagnostic = new MyDiagnostic();
            var configuration = new HandlerRuntimeConfiguration(settings);
            configuration.RegisterDiagnosticSink(myDiagnostic);
            var diagnostics = settings.Get<DiagnosticTypes>();
            Assert.NotNull(diagnostics.Exists( t => t == typeof(MyDiagnostic)));
        }

        [Fact]
        public void Can_register_diagnostic_and_find_it_in_the_container_generic()
        {
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterDiagnosticSink<MyDiagnostic>();
            Assert.NotNull(container.Resolve<IDiagnosticSink>());
            Assert.IsType<MyDiagnostic>(container.Resolve<IDiagnosticSink>());
        }

        [Fact]
        public void Can_register_diagnostic_and_find_it_in_the_container_by_type()
        {
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterDiagnosticSink(typeof(MyDiagnostic));
            Assert.NotNull(container.Resolve<IDiagnosticSink>());
            Assert.IsType<MyDiagnostic>(container.Resolve<IDiagnosticSink>());
        }
    }

    public class MyDiagnostic:IDiagnosticSink
    {
        public void Add(string value) { }

        public Task Flush()
        {
            return null;
        }
    }
}
