using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface IStructuredTraceSink
    {
        Task Buffer(StructuredTrace trace);
        Task Flush();
    }
}
