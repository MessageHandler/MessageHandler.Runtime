using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface IObserveLeases<in T> where T : Lease
    {
        Task OnLeaseAcquired(T lease);
        Task OnLeaseReleased(T lease);
    }
}