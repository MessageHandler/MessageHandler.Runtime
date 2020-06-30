using System;
using System.Threading.Tasks;
using MessageHandler.Runtime.Diagnostics;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;

namespace MessageHandler.Runtime
{
    public class LoggingSink : IStructuredTraceSink
    {
        private readonly ILogger _logger;

        public LoggingSink(ILogger logger)
        {
            _logger = logger;
        }

        public Task Buffer(StructuredTrace trace)
        {
            var state = trace.State != null ? Environment.NewLine + trace.State : string.Empty;
            var message = ($"{trace.When:yyyy-MM-dd HH:mm:ss)} [{trace.Severity}] {trace.Text}{state}");

            if(trace.State is Exception)
            {
                _logger.Log(Translate(trace.Severity), (Exception) trace.State, message);
            }
            else
            {
                _logger.Log(Translate(trace.Severity), message, trace.State);
            }            

            return Task.CompletedTask;
        }

        private LogLevel Translate(StructuredTraceSeverity traceSeverity)
        {
            switch (traceSeverity)
            {
                case StructuredTraceSeverity.Verbose:
                    return LogLevel.Trace;
                case StructuredTraceSeverity.Debug:
                    return LogLevel.Debug;
                case StructuredTraceSeverity.Info:
                    return LogLevel.Information;
                case StructuredTraceSeverity.Warn:
                    return LogLevel.Warning;
                case StructuredTraceSeverity.Error:
                    return LogLevel.Error;
                case StructuredTraceSeverity.Fatal:
                    return LogLevel.Critical;
            }

            return LogLevel.None;
        }

        public Task Flush()
        {
            return Task.CompletedTask;
        }
    }

    public static class ApplicationInsightsSettingsExtensions
    {
        public static ConfigurationRoot AddApplicationInsights(this ConfigurationRoot configuration, TelemetryClient telemetry, ILogger logger, StructuredTraceSeverity severity)
        {
            configuration.Tracing().RegisterSink(new LoggingSink(logger), severity: severity);

            var container = configuration.Settings.GetContainer();
            var diagnosticsSourceListener = new DiagnosticSourceListener(telemetry, configuration.Settings);
            diagnosticsSourceListener.Subscribe();
            container.Register(Lifecycle.Singleton, () => diagnosticsSourceListener);           

            return configuration;
        }
    }
}