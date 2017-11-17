using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface IStartupTask
    {
        Task Run();
    }
}