using System.Threading.Tasks;
using MessageHandler.Runtime;
using MessageHandler.Runtime.Diagnostics;
using Xunit;

namespace unittests.Diagnostics
{
    public class When_tracing_diagnostics
    {
        [Fact]
        public async Task Will_not_invoke_below_treshold()
        {
            var sink = new MockStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(sink, severity: StructuredTraceSeverity.Error);

            var trace = container.Resolve<ITrace>();
            await trace.Add(new StructuredTrace() { Text = "test", Severity = StructuredTraceSeverity.Info}, StructuredTraceCompletionBehavior.Flushed);

            Assert.False(sink.BufferCalled);
        }

        [Fact]
        public async Task Will_invoke_above_treshold()
        {
            var sink = new MockStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(sink, severity: StructuredTraceSeverity.Info);

            var trace = container.Resolve<ITrace>();
            await trace.Add(new StructuredTrace() { Text = "test", Severity = StructuredTraceSeverity.Warn }, StructuredTraceCompletionBehavior.Flushed);

            Assert.True(sink.BufferCalled);
        }

        [Fact]
        public async Task Will_not_invoke_for_other_scope()
        {
            var sink = new MockStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(sink, scope: StructuredTraceScope.Infrastructure, severity: StructuredTraceSeverity.Verbose);

            var trace = container.Resolve<ITrace>();
            await trace.Add(new StructuredTrace() { Text = "test", Scope = StructuredTraceScope.Domain }, StructuredTraceCompletionBehavior.Flushed);

            Assert.False(sink.BufferCalled);
        }

        [Fact]
        public async Task Will_invoke_for_same_scope()
        {
            var sink = new MockStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(sink, scope: StructuredTraceScope.Domain, severity: StructuredTraceSeverity.Verbose);

            var trace = container.Resolve<ITrace>();
            await trace.Add(new StructuredTrace() { Text = "test", Scope = StructuredTraceScope.Domain }, StructuredTraceCompletionBehavior.Flushed);

            Assert.True(sink.BufferCalled);
        }

        public class MockStructuredTraceSink : IStructuredTraceSink
        {
            public bool BufferCalled { get; set; }

            public Task Buffer(StructuredTrace trace)
            {
                BufferCalled = true;

                return Task.CompletedTask;
            }

            public Task Flush()
            {
                return Task.CompletedTask;
            }
        }
    }
}