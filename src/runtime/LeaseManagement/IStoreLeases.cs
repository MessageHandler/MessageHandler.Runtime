using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface IStoreLeases<T> where T : ILease
    {
        Task<T> TryAcquire(string leaseId);
        Task Release(T lease);
        Task Put(T lease);
        Task<IList<T>> List();
    }
}