using System;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public class BufferedConsoleStructuredTraceSink : IStructuredTraceSink
    {
        private readonly StringBuilder _builder = new StringBuilder();
        private int _lineCount = 0;

        public async Task Buffer(StructuredTrace traced)
        {
            string value = traced.State.ToString();
            if (_builder.Capacity + value.Length > _builder.MaxCapacity || _lineCount >= Console.BufferHeight - 1)
            {
                await Flush();
            }

            _builder.AppendLine($"{traced.When:yyyy-MM-dd HH:mm:ss)} [{traced.Severity}] {traced.State}");
            _lineCount++;
            
        }

        public async Task Flush()
        {
            var s = _builder.ToString();
            await Console.Out.WriteAsync(s).ConfigureAwait(false);
            _lineCount = 0;
            _builder.Clear();
        }
    }

}