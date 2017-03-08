using System.Configuration;
using System.Threading.Tasks;
using MessageHandler.Runtime;
using MessageHandler.Runtime.ConfigurationSettings;
using MessageHandler.Runtime.Diagnostics;
using Xunit;

namespace unittests.Diagnostics
{
    public class When_registering_trace_sinks
    {
        [Fact]
        public void Can_register_diagnostics_sink_and_find_it_in_the_container()
        {
            var myDiagnostic = new MyStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(myDiagnostic);
            Assert.NotNull(container.Resolve<IStructuredTraceSink>());
            Assert.IsType<MyStructuredTraceSink>(container.Resolve<IStructuredTraceSink>());
        }

        [Fact]
        public void Can_register_diagnostics_sink_instance_and_find_its_type_in_the_settings()
        {
            var settings = new Settings();
            var myDiagnostic = new MyStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration(settings);
            configuration.Tracing().RegisterSink(myDiagnostic);
            var diagnostics = settings.Get<TraceSinkRegistrations>();
            Assert.NotNull(diagnostics.Exists(t => t.Type == typeof(MyStructuredTraceSink)));
        }

        [Fact]
        public void Can_register_diagnostics_sink_and_find_it_in_the_container_generic()
        {
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink<MyStructuredTraceSink>();
            Assert.NotNull(container.Resolve<IStructuredTraceSink>());
            Assert.IsType<MyStructuredTraceSink>(container.Resolve<IStructuredTraceSink>());
        }

        [Fact]
        public void Can_register_diagnostic_sink_and_find_it_in_the_container_by_type()
        {
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(typeof(MyStructuredTraceSink));
            Assert.NotNull(container.Resolve<IStructuredTraceSink>());
            Assert.IsType<MyStructuredTraceSink>(container.Resolve<IStructuredTraceSink>());
        }

        public class MyStructuredTraceSink : IStructuredTraceSink
        {
            public Task Buffer(StructuredTrace traced)
            {
                return Task.CompletedTask;
            }

            public Task Flush()
            {
                return null;
            }
        }
    }
}