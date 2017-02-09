using System.Threading.Tasks;

namespace MessageHandler.EventProcessing.Runtime
{
    public interface IStartupTask
    {
        Task Run();
    }
}