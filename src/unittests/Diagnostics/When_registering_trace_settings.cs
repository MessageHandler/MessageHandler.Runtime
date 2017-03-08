using MessageHandler.Runtime;
using MessageHandler.Runtime.ConfigurationSettings;
using MessageHandler.Runtime.Diagnostics;
using Xunit;

namespace unittests.Diagnostics
{
    public class When_registering_trace_settings
    {
        [Fact]
        public void Can_register_global_severity_level()
        {
            var settings = new Settings();
            var configuration = new HandlerRuntimeConfiguration(settings);
            configuration.Tracing().SeverityTreshold(StructuredTraceSeverity.Debug);

            Assert.Equal(StructuredTraceSeverity.Debug, settings.GetTraceSeverityTreshold(StructuredTraceScope.Domain));
            Assert.Equal(StructuredTraceSeverity.Debug, settings.GetTraceSeverityTreshold(StructuredTraceScope.Infrastructure));
        }

        [Fact]
        public void Can_register_infrastructure_severity_level()
        {
            var settings = new Settings();
            var configuration = new HandlerRuntimeConfiguration(settings);
            configuration.Tracing().SeverityTreshold(StructuredTraceScope.Infrastructure, StructuredTraceSeverity.Debug);
            Assert.Equal(StructuredTraceSeverity.Debug, settings.GetTraceSeverityTreshold(StructuredTraceScope.Infrastructure));
        }

        [Fact]
        public void Can_register_domain_severity_level()
        {
            var settings = new Settings();
            var configuration = new HandlerRuntimeConfiguration(settings);
            configuration.Tracing().SeverityTreshold(StructuredTraceScope.Domain, StructuredTraceSeverity.Info);

            Assert.Equal(StructuredTraceSeverity.Info, settings.GetTraceSeverityTreshold(StructuredTraceScope.Domain));
        }
    }
}