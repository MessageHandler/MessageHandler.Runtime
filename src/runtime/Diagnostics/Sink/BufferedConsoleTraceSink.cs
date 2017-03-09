using System;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public class BufferedConsoleStructuredTraceSink : IStructuredTraceSink
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

        public Task Buffer(StructuredTrace traced)
        {
            var state = traced.State != null ? Environment.NewLine + traced.State : string.Empty;
            _buffer.Put($"{traced.When:yyyy-MM-dd HH:mm:ss)} [{traced.Severity}] {traced.Text}{state}");
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