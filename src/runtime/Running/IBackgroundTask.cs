using System.Threading.Tasks;

namespace MessageHandler.EventProcessing.Runtime
{
    public interface IBackgroundTask
    {
        Task Start();
        Task Stop();
    }
}