using System.Configuration;
using System.Threading.Tasks;
using MessageHandler.Runtime;
using MessageHandler.Runtime.ConfigurationSettings;
using MessageHandler.Runtime.Diagnostics;
using Xunit;

namespace unittests.Diagnostics
{
    public class When_registering_diagnostic_sinks
    {
        [Fact]
        public void Can_register_diagnostics_sink_and_find_it_in_the_container()
        {
            var myDiagnostic = new MyDiagnosticsSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterDiagnosticSink(myDiagnostic);
            Assert.NotNull(container.Resolve<IDiagnosticsSink>());
            Assert.IsType<MyDiagnosticsSink>(container.Resolve<IDiagnosticsSink>());
        }

        [Fact]
        public void Can_register_diagnostics_sink_instance_and_find_its_type_in_the_settings()
        {
            var settings = new Settings();
            var myDiagnostic = new MyDiagnosticsSink();
            var configuration = new HandlerRuntimeConfiguration(settings);
            configuration.RegisterDiagnosticSink(myDiagnostic);
            var diagnostics = settings.Get<DiagnosticSinkTypes>();
            Assert.NotNull(diagnostics.Exists( t => t == typeof(MyDiagnosticsSink)));
        }

        [Fact]
        public void Can_register_diagnostics_sink_and_find_it_in_the_container_generic()
        {
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterDiagnosticSink<MyDiagnosticsSink>();
            Assert.NotNull(container.Resolve<IDiagnosticsSink>());
            Assert.IsType<MyDiagnosticsSink>(container.Resolve<IDiagnosticsSink>());
        }

        [Fact]
        public void Can_register_diagnostic_sink_and_find_it_in_the_container_by_type()
        {
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterDiagnosticSink(typeof(MyDiagnosticsSink));
            Assert.NotNull(container.Resolve<IDiagnosticsSink>());
            Assert.IsType<MyDiagnosticsSink>(container.Resolve<IDiagnosticsSink>());
        }

        public class MyDiagnosticsSink : IDiagnosticsSink
        {
            public void Add(string value) { }

            public Task Flush()
            {
                return null;
            }
        }
    }

   
}
