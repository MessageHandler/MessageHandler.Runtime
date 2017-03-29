using System;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class BackgroundTaskExtensions
    {
        public static void RegisterBackgroundTask(this HandlerRuntimeConfiguration configuration, IBackgroundTask task)
        {
            var settings = configuration.GetSettings();
            var tasks = settings.GetOrCreate<BackgroundTaskTypes>();
            if (!tasks.Contains(task.GetType()))
            {
                tasks.Add(task.GetType());
            }
            var container = settings.GetContainer();
            container.Register(() => task);
        }

        public static void RegisterBackgroundTask(this HandlerRuntimeConfiguration configuration, Type type)
        {
            var settings = configuration.GetSettings();
            var tasks = settings.GetOrCreate<BackgroundTaskTypes>();
            if (!tasks.Contains(type))
            {
                var container = settings.GetContainer();
                container.Register(type);
                tasks.Add(type);
            }
        }

        public static void RegisterBackgroundTask<T>(this HandlerRuntimeConfiguration configuration)
        {
            configuration.RegisterBackgroundTask(typeof(T));
        }

    }
}