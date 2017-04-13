using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Disruptor;
using Disruptor.Dsl;
using MessageHandler.Runtime.ConfigurationSettings;
using MessageHandler.Runtime.Diagnostics;

namespace MessageHandler.Runtime
{
    internal class MetricsCollector : IMetrics {
        public MetricsCompletionBehavior DefaultCompletionBehavior { get; set; } = MetricsCompletionBehavior.FireAndForget;

        private readonly Disruptor<MetricDisruptorEntry> _disruptor;
        private RingBuffer<MetricDisruptorEntry> _buffer;

        public MetricsCollector(IResolveDependencies container, ISettings settings)
        {
            _disruptor = new Disruptor<MetricDisruptorEntry>(() => new MetricDisruptorEntry(), 256, TaskScheduler.Default, ProducerType.Multi, new BlockingWaitStrategy());

            var sinks = container.ResolveAll<IMetricsSink>();
            foreach (var sink in sinks)
            {
                _disruptor.HandleEventsWith(new MetricEventHandler(sink));
            }
        }

        public Task Track(Metric measured)
        {
            return Track(measured, MetricsCompletionBehavior.FireAndForget);
        }

        public Task Track(Metric measured, MetricsCompletionBehavior completionBehavior)
        {
            var completion = new TaskCompletionSource<bool>();

            _buffer.PublishEvent(new MetricTranslator(completion, completionBehavior), measured);

            if (completionBehavior == MetricsCompletionBehavior.FireAndForget)
            {
                completion.SetResult(true);
            }

            return completion.Task;
        }

        public Task Start(CancellationToken token)
        {
            _buffer = _disruptor.Start();
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            _disruptor.Shutdown();
            return Task.CompletedTask;
        }

        internal class MetricTranslator : IEventTranslatorOneArg<MetricDisruptorEntry, Metric>
        {
            private readonly TaskCompletionSource<bool> _completion;
            private readonly MetricsCompletionBehavior _completionBehavior;

            public MetricTranslator(TaskCompletionSource<bool> completion, MetricsCompletionBehavior completionBehavior)
            {
                _completion = completion;
                _completionBehavior = completionBehavior;
            }

            public void TranslateTo(MetricDisruptorEntry @event, long sequence, Metric measured)
            {
                @event.Measured = measured;
                @event.Completion = _completion;
                @event.CompletionBehavior = _completionBehavior;
            }
        }

        internal class MetricEventHandler : IEventHandler<MetricDisruptorEntry>
        {
            private readonly IMetricsSink _sink;
            private readonly List<TaskCompletionSource<bool>> _toComplete = new List<TaskCompletionSource<bool>>();

            public MetricEventHandler(IMetricsSink sink)
            {
                _sink = sink;
            }

            public void OnEvent(MetricDisruptorEntry data, long sequence, bool endOfBatch)
            {
                try
                {
                    _sink.Buffer(data.Measured).GetAwaiter().GetResult();

                    if (data.CompletionBehavior == MetricsCompletionBehavior.Buffered)
                    {
                        data.Completion.SetResult(true);
                    }
                    else if (data.CompletionBehavior == MetricsCompletionBehavior.Flushed)
                    {
                        _toComplete.Add(data.Completion);
                    }

                    if (endOfBatch)
                    {
                        _sink.Flush().GetAwaiter().GetResult();
                        foreach (var completion in _toComplete)
                        {
                            completion.SetResult(true);
                        }
                        _toComplete.Clear();
                    }

                }
                catch (AggregateException ex)
                {
                    var e = ExceptionDispatchInfo.Capture(ex.Flatten().InnerExceptions.First()).SourceException;
                    data.Completion.SetException(e);
                }
            }

        }

        internal class MetricDisruptorEntry
        {
            public Metric Measured { get; set; }
            public TaskCompletionSource<bool> Completion { get; set; }
            public MetricsCompletionBehavior CompletionBehavior { get; set; }
        }
    }
}