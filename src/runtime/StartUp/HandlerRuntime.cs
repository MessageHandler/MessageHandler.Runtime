using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public class HandlerRuntime
    {
        private readonly List<IBackgroundTask> _backgroundTasks = new List<IBackgroundTask>();
        private readonly List<Task> _runningTasks = new List<Task>();

        private HandlerRuntimeConfiguration _config;
        private readonly IContainer _container;
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();

        private HandlerRuntime(HandlerRuntimeConfiguration config, IContainer container)
        {
            _config = config;
            _container = container;
        }

        public static async Task<HandlerRuntime> Create(HandlerRuntimeConfiguration config)
        {
            var conventions = config.Settings.GetOrCreate<Conventions>();
            foreach (var convention in conventions)
            {
               await convention.Apply(config).ConfigureAwait(false);
            }
            config.LockSettings();

            var container = config.Settings.GetContainer();
            return new HandlerRuntime(config, container);
        }

        public async Task Start()
        {
            var metrics = _container?.Resolve<IMetrics>();
            if (metrics != null) await metrics.Start(_tokenSource.Token);

            var tracer = _container?.Resolve<ITrace>();
            if (tracer != null) await tracer.Start(_tokenSource.Token);

            if (tracer != null) await tracer.Info("Starting startup tasks", StructuredTraceScope.Infrastructure);

            var startupTasks = _container?.ResolveAll<IStartupTask>() ?? new List<IStartupTask>();
            foreach (var task in startupTasks)
            {
                if (tracer != null) await tracer.Info($"Starting startup task '{task.GetType()}'", StructuredTraceScope.Infrastructure);
                _runningTasks.Add(task.Run());
            }

            await Task.WhenAll(_runningTasks).ConfigureAwait(false);

            if (tracer != null) await tracer.Info("Starting background tasks", StructuredTraceScope.Infrastructure);

            _backgroundTasks.AddRange(_container?.ResolveAll<IBackgroundTask>() ?? new List<IBackgroundTask>());
            foreach (var task in _backgroundTasks)
            {
                if (tracer != null) await tracer.Info($"Starting background task '{task.GetType()}'", StructuredTraceScope.Infrastructure);
                var t = Task.Factory.StartNew(() => task.Run(_tokenSource.Token), TaskCreationOptions.LongRunning).Unwrap();
                _runningTasks.Add(t);
            }
        }

        public async Task Stop()
        {
            _tokenSource.Cancel(false);
            var gracePeriod = _config.Settings.GetShutdownGracePeriod();

            var tracer = _container?.Resolve<ITrace>();
            if (tracer != null) await tracer.Info($"Stopping, allowing tasks to complete for {gracePeriod.Seconds} seconds", StructuredTraceScope.Infrastructure);

            var timeoutTask = Task.Delay(gracePeriod);
            var waitingForCompletion = Task.WhenAll(_runningTasks);
            await Task.WhenAny(timeoutTask, waitingForCompletion).ConfigureAwait(false);

            var metrics = _container?.Resolve<IMetrics>();
            if (tracer != null) await tracer.Info($"Stopping metrics collection", StructuredTraceScope.Infrastructure);
            if (metrics != null) await metrics.Stop();

            if (tracer != null) await tracer.Info($"Stopped", StructuredTraceScope.Infrastructure);

            if (tracer != null) await tracer.Stop();
        }
    }
}