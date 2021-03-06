using System;
using System.Threading;
using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface IAllocateLeases<out T> where T : ILease
    {
        Task<bool> ExecuteIfLeaseAcquired(string leaseId, Func<Task> toExecute);

        Task Subscribe(string leaseId, IObserveLeases<T> observer);

        Task Unsubscribe(string leaseId, IObserveLeases<T> observer);

        Task Start(CancellationToken cancellation);

        Task Stop();
    }
}