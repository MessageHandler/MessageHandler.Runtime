using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface ITrace
    {
        Task Add(StructuredTrace traced);
        Task Add(StructuredTrace traced, StructuredTraceCompletionBehavior completionBehavior);
    }
}