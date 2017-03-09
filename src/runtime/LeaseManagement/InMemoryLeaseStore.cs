using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public class InMemoryLeaseStore<T> : IStoreLeases<T> where T : ILease, new()
    {
        private readonly ConcurrentDictionary<string, T> _leases = new ConcurrentDictionary<string, T>();

        public Task<T> TryAcquire(string leaseId)
        {
            T lease = _leases.GetOrAdd(leaseId, s => new T() { LeaseId = leaseId });
            return Task.FromResult(lease);
        }

        public Task Put(T lease)
        {
            _leases.AddOrUpdate(lease.LeaseId, s => lease, (s, o) => lease);
            return Task.CompletedTask;
        }

        public Task Release(T lease)
        {
            T l;
            _leases.TryRemove(lease.LeaseId, out l);
            return Task.CompletedTask;
        }

        public Task<IList<T>> List()
        {
            IList<T> result = _leases.Values.ToList();
            return Task.FromResult(result);
        }

        
    }
}