using System.Collections.Generic;
using System.Threading.Tasks;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;

namespace MessageHandler.EventProcessing.Runtime
{
    public class HandlerRuntime
    {
        private List<IBackgroundTask> _backgroundTasks = new List<IBackgroundTask>();
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

        public async Task Start()
        {
            var startupTasks = _container?.ResolveAll<IStartupTask>()??new List<IStartupTask>();
            foreach (var task in startupTasks)
            {
                await task.Run();
            }
            _backgroundTasks.AddRange(_container?.ResolveAll<IBackgroundTask>()??new List<IBackgroundTask>());
            foreach (var task in _backgroundTasks)
            {
                await task.Start();
            }
        }

        public async Task Stop()
        {
            foreach (var task in _backgroundTasks)
            {
                await task.Stop();
            }
        }
    }
}