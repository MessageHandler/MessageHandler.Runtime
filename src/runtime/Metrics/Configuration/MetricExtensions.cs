using System;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class MetricExtensions
    {
        public static MetricsExtensionPoint Metrics(this ConfigurationRoot configuration)
        {
            var container = configuration.Settings.GetContainer();
            container.Register<MetricsCollector>(Lifecycle.Singleton);
            return new MetricsExtensionPoint(configuration.Settings);
        }

        public static void RegisterSink(this MetricsExtensionPoint configuration, IMetricsSink sink)
        {
            var settings = configuration.GetSettings();
            var metrics = settings.GetOrCreate<MetricSinkTypes>();
            metrics.Add(sink.GetType());
            var container = settings.GetContainer();
            container.Register(() => sink);
        }

        public static void RegisterSink(this MetricsExtensionPoint configuration, Type type)
        {
            var settings = configuration.GetSettings();
            var metrics = settings.GetOrCreate<MetricSinkTypes>();
            var container = settings.GetContainer();
            container.Register(type);
            metrics.Add(type);
        }

        public static void RegisterSink<T>(this MetricsExtensionPoint configuration)
        {
            configuration.RegisterSink(typeof(T));
        }
    }
}
