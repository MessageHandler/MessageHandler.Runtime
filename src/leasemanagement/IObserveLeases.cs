using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface IObserveLeases<in T> where T : ILease
    {
        Task OnLeaseAcquired(T lease);
        Task OnLeaseReleased(T lease);
    }
}