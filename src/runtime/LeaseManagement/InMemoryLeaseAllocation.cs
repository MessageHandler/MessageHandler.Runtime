using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public class InMemoryLeaseAllocation<T> : IAllocateLeases<T> where T : Lease
    {
        private readonly IStoreLeases<T> _leaseStorage;
        private readonly ICreateLeases<T> _leaseFactory;

        private readonly ConcurrentDictionary<string, LeaseAllocation> _allocations = new ConcurrentDictionary<string, LeaseAllocation>();

        private readonly ConcurrentBag<LeaseSubscription> _subscriptions = new ConcurrentBag<LeaseSubscription>();

        private Task _autoAllocationTask;

        public InMemoryLeaseAllocation(IStoreLeases<T> leaseStorage, ICreateLeases<T> leaseFactory)
        {
            _leaseStorage = leaseStorage;
            _leaseFactory = leaseFactory;
        }

        public Task<bool> ExecuteIfLeaseAcquired(string leaseId, Func<Task> toExecute)
        {
            LeaseAllocation allocation;
            _allocations.TryGetValue(leaseId, out allocation);
            if (allocation != null && allocation.Acquired)
            {
                toExecute();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task Start(CancellationToken cancellation)
        {
            _autoAllocationTask = Task.Factory.StartNew(() => AutoAcquire(cancellation), cancellation);

            return Task.CompletedTask;
        }

        public async Task Stop()
        {
            await _autoAllocationTask;
        }

        public Task Subscribe(string leaseId, IObserveLeases<T> observer)
        {
            var existing = _subscriptions.FirstOrDefault(o => o.Hashcode == observer.GetHashCode());
            if (existing == null)
            {
                _subscriptions.Add(new LeaseSubscription
                {
                    LeaseId = leaseId,
                    Hashcode = observer.GetHashCode(),
                    Reference = new WeakReference<IObserveLeases<T>>(observer),
                    Active = true
                });
            }
            else
            {
                existing.Reference = new WeakReference<IObserveLeases<T>>(observer);
                existing.Active = true;
            }

            LeaseAllocation allocation;
            if (_allocations.TryGetValue(leaseId, out allocation) && allocation.Acquired)
            {
                observer.OnLeaseAcquired(allocation.Lease);
            }

            return Task.CompletedTask;
        }

        public Task Unsubscribe(string leaseId, IObserveLeases<T> observer)
        {
            var existing = _subscriptions.FirstOrDefault(o => o.Hashcode == observer.GetHashCode());
            if (existing != null)
            {
                existing.Active = false;
            }

            return Task.CompletedTask;
        }

        public async Task AutoAcquire(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                var tasks = new List<Task>();
                var desiredLeases = _subscriptions.Where(s => s.Active).Select(s => s.LeaseId);
                foreach (var leaseId in desiredLeases)
                {
                    tasks.Add(_leaseStorage.TryAcquire(leaseId));
                }

                await Task.WhenAll(tasks);
                await Allocate();
                await Task.Delay(TimeSpan.FromSeconds(30), cancellation);
            }
        }

        public async Task Allocate()
        {
            var currentlyOwnedLeases = _allocations.Where(a => a.Value.Acquired).Select(a => a.Value.Lease).ToList();
            var leasesToRevoke = new List<T>();
            var obtainedLeases = await _leaseStorage.List();
            foreach (var lease in currentlyOwnedLeases)
            {
                if (obtainedLeases.All(l => l.LeaseId != lease.LeaseId))
                {
                    leasesToRevoke.Add(lease);
                }
            }

            foreach (var lease in obtainedLeases)
            {
                await Grant(lease.LeaseId);
            }

            foreach (var lease in leasesToRevoke)
            {
                await Revoke(lease.LeaseId);
            }
        }

        private async Task Grant(string leaseId)
        {
            var allocation = _allocations.AddOrUpdate(leaseId, 
            s => new LeaseAllocation
            {
                Lease = _leaseFactory.Create(s),
                Acquired = true
            },
            (s, o) =>
            {
                o.Acquired = true;
                return o;
            });

            var toInvoke = _subscriptions.Where(o => o.LeaseId == leaseId && o.Active);
            foreach (var subscription in toInvoke)
            {
                IObserveLeases<T> observer;
                if (subscription.Reference.TryGetTarget(out observer))
                {
                    await observer.OnLeaseAcquired(allocation.Lease);
                }
            }
        }

        private async Task Revoke(string leaseId)
        {
            var allocation = _allocations.AddOrUpdate(leaseId,
            s => new LeaseAllocation
            {
                Lease = null,
                Acquired = false
            },
            (s, o) =>
            {
                o.Acquired = false;
                return o;
            });

            var toInvoke = _subscriptions.Where(o => o.LeaseId == leaseId && o.Active);
            foreach (var subscription in toInvoke)
            {
                IObserveLeases<T> observer;
                if (subscription.Reference.TryGetTarget(out observer))
                {
                    await observer.OnLeaseReleased(allocation.Lease);
                }
            }
        }

        internal class LeaseAllocation
        {
            public T Lease { get; set; }
            public bool Acquired { get; set; }
        }

        internal class LeaseSubscription
        {
            public string LeaseId { get; set; }
            public int Hashcode { get; set; }
            public WeakReference<IObserveLeases<T>> Reference { get; set; }
            public bool Active { get; set; }
        }
    }
}