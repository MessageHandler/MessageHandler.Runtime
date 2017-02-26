using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public class InMemoryLeaseStore<T> : IStoreLeases<T> where T : Lease
    {
        private readonly ConcurrentDictionary<string, T> _leases = new ConcurrentDictionary<string, T>();
        private readonly ICreateLeases<T> _leaseFactory;

        public InMemoryLeaseStore(ICreateLeases<T> leaseFactory)
        {
            _leaseFactory = leaseFactory;
        }

        public Task<T> TryAcquire(string leaseId)
        {
            T lease = _leases.GetOrAdd(leaseId, s => _leaseFactory.Create(s));
            return Task.FromResult(lease);
        }

        public Task Put(T lease)
        {
            _leases.AddOrUpdate(lease.LeaseId, s => lease, (s, o) => lease);
            return Task.CompletedTask;
        }

        public Task Release(string leaseId)
        {
            T lease;
            _leases.TryRemove(leaseId, out lease);
            return Task.CompletedTask;
        }

        public Task<IList<T>> List()
        {
            IList<T> result = _leases.Values.ToList();
            return Task.FromResult(result);
        }

        
    }
}