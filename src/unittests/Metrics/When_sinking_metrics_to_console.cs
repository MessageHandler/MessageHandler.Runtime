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
    public class When_sinking_metrics_to_console
    {
        [Fact]
        public async Task Can_sink_fast()
        {
            var myDiagnostic = new BufferedConsoleMetricsSink();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.Metrics().RegisterSink(myDiagnostic);
            
            var metrics = container.Resolve<IMetrics>();
            await metrics.Start(CancellationToken.None);
            var sw = new Stopwatch();
            sw.Start();

            int numberOfMetrics = 100;
            
            for (int i = 0; i < numberOfMetrics; i++)
            {
               await metrics.Track(new Metric( "SomeMetric", i));
            }

            sw.Stop();
            await metrics.Stop();
            Assert.True(sw.ElapsedMilliseconds < 100, "Sinking metric to console took " + sw.ElapsedMilliseconds + " milliseconds.");
        }
    }
}