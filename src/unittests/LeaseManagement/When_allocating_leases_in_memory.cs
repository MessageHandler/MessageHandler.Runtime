using System.Threading.Tasks;
using MessageHandler.Runtime;
using Xunit;

namespace unittests
{
    public class When_allocating_leases_in_memory
    {
        [Fact]
        public async Task Will_execute_if_lease_granted()
        {
            int invocationCount = 0;
            var leaseStore = new InMemoryLeaseStore<Lease>(new DefaultLeaseCreation<Lease>());
            var leases = new InMemoryLeaseAllocation<Lease>(leaseStore, new DefaultLeaseCreation<Lease>());
            await leaseStore.TryAcquire("mylease");
            await leases.Allocate();
            await leases.ExecuteIfLeaseAcquired("mylease", () => Task.FromResult(invocationCount++));
            Assert.Equal(1, invocationCount);
        }

        [Fact]
        public async Task Will_not_execute_if_lease_not_granted()
        {
            int invocationCount = 0;
            var leaseStore = new InMemoryLeaseStore<Lease>(new DefaultLeaseCreation<Lease>());
            var leases = new InMemoryLeaseAllocation<Lease>(leaseStore, new DefaultLeaseCreation<Lease>());
            await leases.Allocate();
            await leases.ExecuteIfLeaseAcquired("mylease", () => Task.FromResult(invocationCount++));
            Assert.Equal(0, invocationCount);
        }

        [Fact]
        public async Task Will_not_execute_if_lease_revoked()
        {
            int invocationCount = 0;
            var leaseStore = new InMemoryLeaseStore<Lease>(new DefaultLeaseCreation<Lease>());
            var leases = new InMemoryLeaseAllocation<Lease>(leaseStore, new DefaultLeaseCreation<Lease>());
            await leaseStore.TryAcquire("mylease");
            await leases.Allocate();
            await leaseStore.Release("mylease");
            await leases.Allocate();
            await leases.ExecuteIfLeaseAcquired("mylease", () => Task.FromResult(invocationCount++));
            Assert.Equal(0, invocationCount);
        }

        [Fact]
        public async Task Will_invoke_on_lease_granted_on_observer()
        {
            var leaseStore = new InMemoryLeaseStore<Lease>(new DefaultLeaseCreation<Lease>());
            var leases = new InMemoryLeaseAllocation<Lease>(leaseStore, new DefaultLeaseCreation<Lease>());
            var observer = new MyLeaseObserver();
            await leases.Subscribe("mylease", observer);
            await leaseStore.TryAcquire("mylease");
            await leases.Allocate();
            Assert.True(observer.LeaseAcquiredCalled);
        }

        [Fact]
        public async Task Will_invoke_on_lease_released_on_observer()
        {
            var leaseStore = new InMemoryLeaseStore<Lease>(new DefaultLeaseCreation<Lease>());
            var leases = new InMemoryLeaseAllocation<Lease>(leaseStore, new DefaultLeaseCreation<Lease>());
            var observer = new MyLeaseObserver();
            await leases.Subscribe("mylease", observer);
            await leaseStore.TryAcquire("mylease");
            await leases.Allocate();
            await leaseStore.Release("mylease");
            await leases.Allocate();
            Assert.True(observer.LeaseReleasedCalled);
        }

        [Fact]
        public async Task Will_not_invoke_on_lease_granted_on_observer_after_unsubscribe()
        {
            var leaseStore = new InMemoryLeaseStore<Lease>(new DefaultLeaseCreation<Lease>());
            var leases = new InMemoryLeaseAllocation<Lease>(leaseStore, new DefaultLeaseCreation<Lease>());
            var observer = new MyLeaseObserver();
            await leases.Subscribe("mylease", observer);
            await leases.Unsubscribe("mylease", observer);
            await leaseStore.TryAcquire("mylease");
            await leases.Allocate();
            Assert.False(observer.LeaseAcquiredCalled);
        }

        [Fact]
        public async Task Will_not_invoke_on_lease_released_on_observer_after_unsubscribe()
        {
            var leaseStore = new InMemoryLeaseStore<Lease>(new DefaultLeaseCreation<Lease>());
            var leases = new InMemoryLeaseAllocation<Lease>(leaseStore, new DefaultLeaseCreation<Lease>());
            var observer = new MyLeaseObserver();
            await leases.Subscribe("mylease", observer);
            await leases.Unsubscribe("mylease", observer);
            await leaseStore.TryAcquire("mylease");
            await leases.Allocate();
            await leaseStore.Release("mylease");
            await leases.Allocate();
            Assert.False(observer.LeaseReleasedCalled);
        }
        
        public class MyLeaseObserver : IObserveLeases<Lease>
        {
            public bool LeaseAcquiredCalled { get; set; }
            public bool LeaseReleasedCalled { get; set; }

            public Task OnLeaseAcquired(Lease lease)
            {
                LeaseAcquiredCalled = true;
                return Task.CompletedTask;
            }

            public Task OnLeaseReleased(Lease lease)
            {
                LeaseReleasedCalled = true;
                return Task.CompletedTask;
            }
        }
    }
}