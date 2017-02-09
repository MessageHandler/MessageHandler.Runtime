using System.Collections.Generic;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;

namespace MessageHandler.EventProcessing.Runtime
{
    public class HandlerRuntime
    {
        private HandlerRuntimeConfiguration _config;
        private IContainer _container;

        private HandlerRuntime(HandlerRuntimeConfiguration config, IContainer container)
        {
            _config = config;
            _container = container;
        }
        public static HandlerRuntime Create(HandlerRuntimeConfiguration config)
        {
            config.LockSettings();

            var container = config.Settings.Get<IContainer>("messagehandler.container");
            var runtime = new HandlerRuntime(config, container);
            

            container?.Register(() => config.Settings);

            return runtime;
        }

        public void Start()
        {
            var tasks = _container?.ResolveAll<IStartupTask>()??new List<IStartupTask>();
            foreach (var task in tasks)
            {
                task.Run();
            }
        }
    }
}