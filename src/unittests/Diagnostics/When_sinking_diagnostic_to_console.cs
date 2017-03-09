using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MessageHandler.Runtime;
using MessageHandler.Runtime.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace unittests.Diagnostics
{
    public class When_tracing_diagnostic_to_console
    {
        [Fact]
        public async Task Can_trace_fast()
        {
            var myDiagnostic = new BufferedConsoleStructuredTraceSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Tracing().RegisterSink(myDiagnostic);
            
            var trace = container.Resolve<ITrace>();
            await trace.Start(CancellationToken.None);
            var sw = new Stopwatch();
            sw.Start();

            int numberOfTraces = 10000;
            
            for (int i = 0; i < numberOfTraces; i++)
            {
               await trace.Add(new StructuredTrace() { Text = i.ToString() });
            }

            sw.Stop();
            await trace.Stop();
            Assert.True(sw.ElapsedMilliseconds < 100, "Tracing to console took " + sw.ElapsedMilliseconds + " milliseconds.");
        }
    }
}