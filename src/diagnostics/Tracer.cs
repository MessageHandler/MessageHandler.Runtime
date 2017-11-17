using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Disruptor;
using Disruptor.Dsl;
using MessageHandler.Runtime.ConfigurationSettings;
using MessageHandler.Runtime.Diagnostics;

namespace MessageHandler.Runtime
{
    internal class Tracer : ITrace
    {
        private readonly Disruptor<TraceDisruptorEntry> _disruptor;
        private RingBuffer<TraceDisruptorEntry> _buffer;
       
        public Tracer(IResolveDependencies container, ISettings settings)
        {
            _disruptor = new Disruptor<TraceDisruptorEntry>(() => new TraceDisruptorEntry(), 16384, TaskScheduler.Default, ProducerType.Multi, new BlockingWaitStrategy());

            var sinks = container.ResolveAll<IStructuredTraceSink>();
            var registrations = settings.Get<TraceSinkRegistrations>();
            foreach (var sink in sinks)
            {
                var registration = registrations.First(r => r.Type == sink.GetType());
                _disruptor.HandleEventsWith(new TraceEventHandler(sink, registration));
            }
        }

        public StructuredTraceCompletionBehavior DefaultCompletionBehavior { get; set; } = StructuredTraceCompletionBehavior.FireAndForget;

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

        [MethodImpl(MethodImplOptions.NoInlining)]
        public Task Verbose(string text, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "")
        {
            var where = !string.IsNullOrEmpty(callerName) ? callerName : new StackTrace().GetFrame(1).GetMethod().Name;

            return Add(new StructuredTrace
            {
                Scope = scope,
                Severity = StructuredTraceSeverity.Verbose,
                Text = text,
                Where = where,
                What = "Trace"
            }, DefaultCompletionBehavior);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Task Debug(string text, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "")
        {
            var where = !string.IsNullOrEmpty(callerName) ? callerName : new StackTrace().GetFrame(1).GetMethod().Name;

            return Add(new StructuredTrace()
            {
                Scope = scope,
                Severity = StructuredTraceSeverity.Debug,
                Text = text,
                Where = where,
                What = "Trace"
            }, DefaultCompletionBehavior);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Task Info(string text, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "")
        {
            var where = !string.IsNullOrEmpty(callerName) ? callerName : new StackTrace().GetFrame(1).GetMethod().Name;

            return Add(new StructuredTrace()
            {
                Scope = scope,
                Severity = StructuredTraceSeverity.Info,
                Text = text,
                Where = where,
                What = "Trace"
            }, DefaultCompletionBehavior);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Task Warn(string text, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "")
        {
            var where = !string.IsNullOrEmpty(callerName) ? callerName : new StackTrace().GetFrame(1).GetMethod().Name;

            return Add(new StructuredTrace()
            {
                Scope = scope,
                Severity = StructuredTraceSeverity.Warn,
                Text = text,
                Where = where,
                What = "Trace"
            }, DefaultCompletionBehavior);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Task Error(string text, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "")
        {
            var where = !string.IsNullOrEmpty(callerName) ? callerName : new StackTrace().GetFrame(1).GetMethod().Name;

            return Add(new StructuredTrace
            {
                Scope = scope,
                Severity = StructuredTraceSeverity.Error,
                Text = text,
                Where = where,
                What = "Trace"
            }, StructuredTraceCompletionBehavior.Flushed);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Task Error(string text, Exception exception, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "")
        {
            var where = !string.IsNullOrEmpty(callerName) ? callerName : new StackTrace().GetFrame(1).GetMethod().Name;

            return Add(new StructuredTrace
            {
                Scope = scope,
                Severity = StructuredTraceSeverity.Error,
                State = exception,
                Text = text,
                Where = where,
                What = "Trace"
            }, StructuredTraceCompletionBehavior.Flushed);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Task Fatal(string text, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "")
        {
            var where = !string.IsNullOrEmpty(callerName) ? callerName : new StackTrace().GetFrame(1).GetMethod().Name;

            return Add(new StructuredTrace
            {
                Scope = scope,
                Severity = StructuredTraceSeverity.Fatal,
                Text = text,
                Where = where,
                What = "Trace"
            }, StructuredTraceCompletionBehavior.Flushed);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        public Task Fatal(string text, Exception exception, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "")
        {
            var where = !string.IsNullOrEmpty(callerName) ? callerName : new StackTrace().GetFrame(1).GetMethod().Name;

            return Add(new StructuredTrace
            {
                Scope = scope,
                Severity = StructuredTraceSeverity.Fatal,
                Text = text,
                State = exception,
                Where = where,
                What = "Trace"
            }, StructuredTraceCompletionBehavior.Flushed);
        }

        public Task Add(StructuredTrace traced)
        {
            return Add(traced, StructuredTraceCompletionBehavior.FireAndForget);
        }

        public Task Add(StructuredTrace traced, StructuredTraceCompletionBehavior completionBehavior)
        {
            var completion = new TaskCompletionSource<bool>();

            _buffer.PublishEvent(new TraceTranslator(completion, completionBehavior), traced);

            if (completionBehavior == StructuredTraceCompletionBehavior.FireAndForget)
            {
                completion.SetResult(true);
            }

            return completion.Task;
        }

        internal class TraceTranslator : IEventTranslatorOneArg<TraceDisruptorEntry, StructuredTrace>
        {
            private readonly TaskCompletionSource<bool> _completion;
            private readonly StructuredTraceCompletionBehavior _completionBehavior;

            public TraceTranslator(TaskCompletionSource<bool> completion, StructuredTraceCompletionBehavior completionBehavior)
            {
                _completion = completion;
                _completionBehavior = completionBehavior;
            }

            public void TranslateTo(TraceDisruptorEntry @event, long sequence, StructuredTrace traced)
            {
                @event.Traced = traced;
                @event.Completion = _completion;
                @event.CompletionBehavior = _completionBehavior;
            }
        }

        internal class TraceEventHandler : IEventHandler<TraceDisruptorEntry>
        {
            private readonly IStructuredTraceSink _sink;
            private readonly TraceSinkRegistration _registration;
            private readonly List<TaskCompletionSource<bool>> _toComplete = new List<TaskCompletionSource<bool>>();

            public TraceEventHandler(IStructuredTraceSink sink, TraceSinkRegistration registration)
            {
                _sink = sink;
                _registration = registration;
            }

            public void OnEvent(TraceDisruptorEntry data, long sequence, bool endOfBatch)
            {
                try
                {
                    if (data.Traced.Severity >= _registration.Severity && _registration.Scope.HasFlag(data.Traced.Scope))
                    {
                        _sink.Buffer(data.Traced).GetAwaiter().GetResult();
                    }

                    if (data.CompletionBehavior == StructuredTraceCompletionBehavior.Buffered)
                    {
                        data.Completion.SetResult(true);
                    }
                    else if (data.CompletionBehavior == StructuredTraceCompletionBehavior.Flushed)
                    {
                        _toComplete.Add(data.Completion);
                    }

                    if (endOfBatch )
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

        internal class TraceDisruptorEntry
        {
            public StructuredTrace Traced { get; set; }
            public TaskCompletionSource<bool> Completion { get; set; }
            public StructuredTraceCompletionBehavior CompletionBehavior { get; set; }
        }
    }
}