using System;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;

namespace MessageHandler.EventProcessing.Runtime
{
    public static class StartupTaskExtensions
    {
        public static void RegisterStartupTask(this HandlerRuntimeConfiguration configuration, IStartupTask task)
        {
            var settings = configuration.GetSettings();
            var tasks = settings.GetOrCreate<StartupTaskTypes>();
            if (tasks.Contains(task.GetType()))
            {
                throw new StartupTaskRegisteredException("Startup task is already registered.");
            }
            tasks.Add(task.GetType());
            var container = settings.Get<IContainer>("messagehandler.container");
            container.Register(() => task);
        }

        public static void RegisterStartupTask(this HandlerRuntimeConfiguration configuration, Type type)
        {
            var settings = configuration.GetSettings();
            var tasks = settings.GetOrCreate<StartupTaskTypes>();
            if (tasks.Contains(type))
            {
                throw new StartupTaskRegisteredException("Startup task is already registered.");
            }
            var container = settings.Get<IContainer>("messagehandler.container");
            container.Register(type);
            tasks.Add(type);
        }

        public static void RegisterStartupTask<T>(this HandlerRuntimeConfiguration configuration)
        {
            configuration.RegisterStartupTask(typeof(T));
        }

    }
}
