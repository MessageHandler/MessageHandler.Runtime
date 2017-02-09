using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MessageHandler.EventProcessing.Runtime
{
    public class HandlerRuntime
    {
        private List<IBackgroundTask> _backgroundTasks = new List<IBackgroundTask>();
        private List<Task> _runningTasks = new List<Task>();

        private HandlerRuntimeConfiguration _config;
        private IContainer _container;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();


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
            var startupTasks = _container?.ResolveAll<IStartupTask>() ?? new List<IStartupTask>();
            foreach (var task in startupTasks)
            {
                await task.Run();
            }

            _backgroundTasks.AddRange(_container?.ResolveAll<IBackgroundTask>() ?? new List<IBackgroundTask>());
            foreach (var task in _backgroundTasks)
            {
                var t = Task.Run(() => task.Run(_tokenSource.Token), CancellationToken.None);
                _runningTasks.Add(t);
            }
        }

        public async Task Stop()
        {
            _tokenSource.Cancel(false);
            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(30));
            var waitingForCompletion = Task.WhenAll(_runningTasks);
            await Task.WhenAny(timeoutTask, waitingForCompletion);
        }
    }
}