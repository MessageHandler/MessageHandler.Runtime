using System;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public class BufferedConsoleMetricsSink : IMetricsSink
    {
        private readonly CircularBuffer<string> _buffer = new CircularBuffer<string>(GetBufferHeight());

        private static int GetBufferHeight()
        {
            try
            {
                return Console.BufferHeight;
            }
            catch
            {
                return 300; // default console bufferheight, if console not available or redirected.
            }
        }

        public Task Buffer(Metric measured)
        {
            _buffer.Put($"{measured.Name}: {measured.Value}");
            return Task.CompletedTask;
        }
        
        public async Task Flush()
        {
            var toFlush = _buffer.Clear();
            var builder = new StringBuilder();
            foreach (var flush in toFlush)
            {
                builder.AppendLine(flush);
            }
            await Console.Out.WriteAsync(builder.ToString()).ConfigureAwait(false);
        }

        
    }
}