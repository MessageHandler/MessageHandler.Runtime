using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface IDiagnosticsSink
    {
        void Add(string value);
        Task Flush();
    }
}
