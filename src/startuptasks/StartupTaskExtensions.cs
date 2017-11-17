using System;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class StartupTaskExtensions
    {
        public static void RegisterStartupTask(this ConfigurationRoot configuration, IStartupTask task)
        {
            var settings = configuration.GetSettings();
            settings.RegisterStartupTask(task);
        }

        public static void RegisterStartupTask(this ConfigurationRoot configuration, Type type)
        {
            var settings = configuration.GetSettings();
            settings.RegisterStartupTask(type);
        }

        public static void RegisterStartupTask<T>(this ConfigurationRoot configuration)
        {
            configuration.RegisterStartupTask(typeof(T));
        }

        public static void RegisterStartupTask(this ISettings settings, IStartupTask task)
        {
            var tasks = settings.GetOrCreate<StartupTaskTypes>();
            if (!tasks.Contains(task.GetType()))
            {
                tasks.Add(task.GetType());
            }
            var container = settings.GetContainer();
            container.Register(() => task);
        }

        public static void RegisterStartupTask(this ISettings settings, Type type)
        {
            var tasks = settings.GetOrCreate<StartupTaskTypes>();
            if (!tasks.Contains(type))
            {
                var container = settings.GetContainer();
                container.Register(type);
                tasks.Add(type);
            }
        }

        public static void RegisterStartupTask<T>(this ISettings settings)
        {
            settings.RegisterStartupTask(typeof(T));
        }

    }
}
