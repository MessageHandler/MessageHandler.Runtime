using System;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime.Diagnostics
{
    public static class TraceExtensions
    {
        public static TraceSettingsExtensionPoint Tracing(this HandlerRuntimeConfiguration configuration)
        {
            var container = configuration.Settings.GetContainer();
            container.Register<Tracer>(Lifecycle.Singleton);
            return new TraceSettingsExtensionPoint(configuration.Settings);
        }

        public static void SeverityTreshold(this TraceSettingsExtensionPoint configuration, StructuredTraceSeverity severity)
        {
            SeverityTreshold(configuration, StructuredTraceScope.Infrastructure, severity);
            SeverityTreshold(configuration, StructuredTraceScope.Domain, severity);
        }

        public static void SeverityTreshold(this TraceSettingsExtensionPoint configuration, StructuredTraceScope scope, StructuredTraceSeverity severity)
        {
            var settings = configuration.GetSettings();
            var tresholds = settings.GetOrCreate<TraceSeverityTresholds>();
            tresholds[scope] = severity;
        }

        public static void RegisterSink(this TraceSettingsExtensionPoint configuration, IStructuredTraceSink sink, StructuredTraceScope scope = StructuredTraceScope.Domain, StructuredTraceSeverity severity = StructuredTraceSeverity.Warn)
        {
            var settings = configuration.GetSettings();
            var registrations = settings.GetOrCreate<TraceSinkRegistrations>();
            registrations.Add(new TraceSinkRegistration()
            {
                Type = sink.GetType(),
                Scope = scope,
                Severity = severity
            });
            var container = settings.GetContainer();
            container.Register(()=>sink);
        }

        public static void RegisterSink(this TraceSettingsExtensionPoint configuration, Type type, StructuredTraceScope scope = StructuredTraceScope.Domain, StructuredTraceSeverity severity = StructuredTraceSeverity.Warn)
        {
            var settings = configuration.GetSettings();
            var registrations = settings.GetOrCreate<TraceSinkRegistrations>();
            registrations.Add(new TraceSinkRegistration()
            {
                Type = type,
                Scope = scope,
                Severity = severity
            });
            var container = settings.GetContainer();
            container.Register(type);
        }

        public static void RegisterSink<T>(this TraceSettingsExtensionPoint configuration, StructuredTraceScope scope = StructuredTraceScope.Domain, StructuredTraceSeverity severity = StructuredTraceSeverity.Warn)
        {
            RegisterSink(configuration, typeof(T), scope, severity);
        }

        internal static StructuredTraceSeverity GetTraceSeverityTreshold(this ISettings settings, StructuredTraceScope scope)
        {
            var tresholds = settings.GetOrCreate<TraceSeverityTresholds>();
            return tresholds.ContainsKey(scope) ? tresholds[scope] : StructuredTraceSeverity.Verbose;
        }
    }
}
