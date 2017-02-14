using System;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime.Diagnostics
{
    public static class DiagnosticTaskExtensions
    {
        public static void RegisterDiagnosticSink(this HandlerRuntimeConfiguration configuration, IDiagnosticSink diagnostic)
        {
            var settings = configuration.GetSettings();
            var diagnostics = settings.GetOrCreate<DiagnosticTypes>();
            diagnostics.Add(diagnostic.GetType());
            var container = settings.GetContainer();
            container.Register(()=>diagnostic);
        }

        public static void RegisterDiagnosticSink(this HandlerRuntimeConfiguration configuration, Type type)
        {
            var settings = configuration.GetSettings();
            var diagnostics = settings.GetOrCreate<DiagnosticTypes>();
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
