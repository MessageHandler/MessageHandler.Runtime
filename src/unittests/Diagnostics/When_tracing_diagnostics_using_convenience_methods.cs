using System;
using System.Threading;
using System.Threading.Tasks;
using MessageHandler.Runtime;
using MessageHandler.Runtime.Diagnostics;
using Xunit;

namespace unittests.Diagnostics
{
    public class When_tracing_diagnostics_using_convenience_methods
    {
        [Fact]
        public async Task Will_trace_verbose()
        {
            var start = DateTimeOffset.UtcNow;
            var sink = new MockStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(sink, severity: StructuredTraceSeverity.Verbose);

            var trace = container.Resolve<ITrace>();
            //change the default completion behavior so that we can validate the outcome
            trace.DefaultCompletionBehavior = StructuredTraceCompletionBehavior.Buffered;
            await trace.Start(CancellationToken.None);
            await trace.Verbose("test");
            await trace.Stop();

            Assert.Equal(StructuredTraceSeverity.Verbose, sink.Traced.Severity);
            Assert.Equal(StructuredTraceScope.Domain, sink.Traced.Scope);
            Assert.Equal("test", sink.Traced.Text);
            Assert.Equal(nameof(Will_trace_verbose), sink.Traced.Where);
            Assert.True(sink.Traced.When >= start && sink.Traced.When <= DateTimeOffset.UtcNow);
        }

        [Fact]
        public async Task Will_trace_debug()
        {
            var start = DateTimeOffset.UtcNow;
            var sink = new MockStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(sink, severity: StructuredTraceSeverity.Verbose);

            var trace = container.Resolve<ITrace>();
            //change the default completion behavior so that we can validate the outcome
            trace.DefaultCompletionBehavior = StructuredTraceCompletionBehavior.Buffered;
            await trace.Start(CancellationToken.None);
            await trace.Debug("test");
            await trace.Stop();

            Assert.Equal(StructuredTraceSeverity.Debug, sink.Traced.Severity);
            Assert.Equal(StructuredTraceScope.Domain, sink.Traced.Scope);
            Assert.Equal("test", sink.Traced.Text);
            Assert.Equal(nameof(Will_trace_debug), sink.Traced.Where);
            Assert.True(sink.Traced.When >= start && sink.Traced.When <= DateTimeOffset.UtcNow);
        }

        [Fact]
        public async Task Will_trace_info()
        {
            var start = DateTimeOffset.UtcNow;
            var sink = new MockStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(sink, severity: StructuredTraceSeverity.Verbose);

            var trace = container.Resolve<ITrace>();
            //change the default completion behavior so that we can validate the outcome
            trace.DefaultCompletionBehavior = StructuredTraceCompletionBehavior.Buffered;
            await trace.Start(CancellationToken.None);
            await trace.Info("test");
            await trace.Stop();

            Assert.Equal(StructuredTraceSeverity.Info, sink.Traced.Severity);
            Assert.Equal(StructuredTraceScope.Domain, sink.Traced.Scope);
            Assert.Equal("test", sink.Traced.Text);
            Assert.Equal(nameof(Will_trace_info), sink.Traced.Where);
            Assert.True(sink.Traced.When >= start && sink.Traced.When <= DateTimeOffset.UtcNow);
        }

        [Fact]
        public async Task Will_trace_warn()
        {
            var start = DateTimeOffset.UtcNow;
            var sink = new MockStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(sink, severity: StructuredTraceSeverity.Verbose);

            var trace = container.Resolve<ITrace>();
            //change the default completion behavior so that we can validate the outcome
            trace.DefaultCompletionBehavior = StructuredTraceCompletionBehavior.Buffered;
            await trace.Start(CancellationToken.None);
            await trace.Warn("test");
            await trace.Stop();

            Assert.Equal(StructuredTraceSeverity.Warn, sink.Traced.Severity);
            Assert.Equal(StructuredTraceScope.Domain, sink.Traced.Scope);
            Assert.Equal("test", sink.Traced.Text);
            Assert.Equal(nameof(Will_trace_warn), sink.Traced.Where);
            Assert.True(sink.Traced.When >= start && sink.Traced.When <= DateTimeOffset.UtcNow);
        }

        [Fact]
        public async Task Will_trace_error()
        {
            var start = DateTimeOffset.UtcNow;
            var sink = new MockStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(sink, severity: StructuredTraceSeverity.Verbose);

            var trace = container.Resolve<ITrace>();
            await trace.Start(CancellationToken.None);
            await trace.Error("test");
            await trace.Stop();

            Assert.Equal(StructuredTraceSeverity.Error, sink.Traced.Severity);
            Assert.Equal(StructuredTraceScope.Domain, sink.Traced.Scope);
            Assert.Equal("test", sink.Traced.Text);
            Assert.Equal(nameof(Will_trace_error), sink.Traced.Where);
            Assert.True(sink.Traced.When >= start && sink.Traced.When <= DateTimeOffset.UtcNow);
            Assert.True(sink.WaitedForFlush);
        }

        [Fact]
        public async Task Will_trace_error_with_exception()
        {
            var start = DateTimeOffset.UtcNow;
            var sink = new MockStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(sink, severity: StructuredTraceSeverity.Verbose);

            var trace = container.Resolve<ITrace>();

            var ex = new Exception();
            await trace.Start(CancellationToken.None);
            await trace.Error("test", ex);
            await trace.Stop();

            Assert.Equal(StructuredTraceSeverity.Error, sink.Traced.Severity);
            Assert.Equal(StructuredTraceScope.Domain, sink.Traced.Scope);
            Assert.Equal("test", sink.Traced.Text);
            Assert.Equal(ex, sink.Traced.State);
            Assert.Equal(nameof(Will_trace_error_with_exception), sink.Traced.Where);
            Assert.True(sink.Traced.When >= start && sink.Traced.When <= DateTimeOffset.UtcNow);
            Assert.True(sink.WaitedForFlush);
        }

        [Fact]
        public async Task Will_trace_fatal()
        {
            var start = DateTimeOffset.UtcNow;
            var sink = new MockStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(sink, severity: StructuredTraceSeverity.Verbose);

            var trace = container.Resolve<ITrace>();
            await trace.Start(CancellationToken.None);
            await trace.Fatal("test");
            await trace.Stop();

            Assert.Equal(StructuredTraceSeverity.Fatal, sink.Traced.Severity);
            Assert.Equal(StructuredTraceScope.Domain, sink.Traced.Scope);
            Assert.Equal("test", sink.Traced.Text);
            Assert.Equal(nameof(Will_trace_fatal), sink.Traced.Where);
            Assert.True(sink.Traced.When >= start && sink.Traced.When <= DateTimeOffset.UtcNow);
            Assert.True(sink.WaitedForFlush);
        }

        [Fact]
        public async Task Will_trace_fatal_with_exception()
        {
            var start = DateTimeOffset.UtcNow;
            var sink = new MockStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(sink, severity: StructuredTraceSeverity.Verbose);

            var trace = container.Resolve<ITrace>();
            await trace.Start(CancellationToken.None);
            var ex = new Exception();
            await trace.Fatal("test", ex);
            await trace.Stop();

            Assert.Equal(StructuredTraceSeverity.Fatal, sink.Traced.Severity);
            Assert.Equal(StructuredTraceScope.Domain, sink.Traced.Scope);
            Assert.Equal("test", sink.Traced.Text);
            Assert.Equal(ex, sink.Traced.State);
            Assert.Equal(nameof(Will_trace_fatal_with_exception), sink.Traced.Where);
            Assert.True(sink.Traced.When >= start && sink.Traced.When <= DateTimeOffset.UtcNow);
            Assert.True(sink.WaitedForFlush);
        }

        public class MockStructuredTraceSink : IStructuredTraceSink
        {
            public StructuredTrace Traced { get; set; }
            public bool WaitedForFlush { get; set; }

            public Task Buffer(StructuredTrace trace)
            {
                Traced = trace;

                return Task.CompletedTask;
            }

            public Task Flush()
            {
                WaitedForFlush = true;
                return Task.CompletedTask;
            }
        }
    }
}