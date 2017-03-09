using System;
using System.Diagnostics;
using MessageHandler.Runtime;
using MessageHandler.Runtime.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace unittests.Diagnostics
{
    public class When_tracing_diagnostic_to_console
    {
        [Fact]
        public void Can_trace_fast()
        {
            var myDiagnostic = new BufferedConsoleStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(myDiagnostic);
            
            var trace = container.Resolve<ITrace>();

            var sw = new Stopwatch();
            sw.Start();

            int numberOfTraces = 10000;
            
            for (int i = 0; i < numberOfTraces; i++)
            {
                trace.Add(new StructuredTrace() { Text = i.ToString() });
            }

            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 100, "Tracing to console took " + sw.ElapsedMilliseconds + " milliseconds.");
        }
    }
}