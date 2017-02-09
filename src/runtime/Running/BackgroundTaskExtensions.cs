using System;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;

namespace MessageHandler.EventProcessing.Runtime
{
    public static class BackgroundTaskExtensions
    {
        public static void RegisterBackgroundTask(this HandlerRuntimeConfiguration configuration, IBackgroundTask task)
        {
            var settings = configuration.GetSettings();
            var tasks = settings.GetOrCreate<BackgroundTaskTypes>();
            tasks.Add(task.GetType());
            var container = settings.Get<IContainer>("messagehandler.container");
            container.Register(() => task);
        }

        public static void RegisterBackgroundTask(this HandlerRuntimeConfiguration configuration, Type type)
        {
            var settings = configuration.GetSettings();
            var tasks = settings.GetOrCreate<BackgroundTaskTypes>();
            var container = settings.Get<IContainer>("messagehandler.container");
            container.Register(type);
            tasks.Add(type);
        }

        public static void RegisterBackgroundTask<T>(this HandlerRuntimeConfiguration configuration)
        {
            configuration.RegisterBackgroundTask(typeof(T));
        }

    }
}