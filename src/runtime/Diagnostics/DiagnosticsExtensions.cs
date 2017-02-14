using System;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime.Diagnostics
{
    public static class DiagnosticsExtensions
    {
        public static void RegisterDiagnosticSink(this HandlerRuntimeConfiguration configuration, IDiagnosticsSink sink)
        {
            var settings = configuration.GetSettings();
            var diagnostics = settings.GetOrCreate<DiagnosticSinkTypes>();
            diagnostics.Add(sink.GetType());
            var container = settings.GetContainer();
            container.Register(()=>sink);
        }

        public static void RegisterDiagnosticSink(this HandlerRuntimeConfiguration configuration, Type type)
        {
            var settings = configuration.GetSettings();
            var diagnostics = settings.GetOrCreate<DiagnosticSinkTypes>();
            var container = settings.GetContainer();
            container.Register(type);
            diagnostics.Add(type);
        }

        public static void RegisterDiagnosticSink<T>(this HandlerRuntimeConfiguration configuration)
        {
            configuration.RegisterMetricSink(typeof(T));
        }
    }
}
