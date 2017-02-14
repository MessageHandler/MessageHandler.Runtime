using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface IDiagnosticSink
    {
        void Add(string value);
        Task Flush();
    }
}
