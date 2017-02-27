using System;
using System.Linq;
using System.Threading.Tasks;
using MessageHandler.Runtime;
using Xunit;

namespace unittests
{
    public class When_storing_leases_in_memory
    {
        [Fact]
        public async Task Can_put_lease()
        {
            var leaseId = Guid.NewGuid().ToString();
            var leaseStore = new InMemoryLeaseStore<MyLease>(new DefaultLeaseCreation<MyLease>());
            await leaseStore.Put(new MyLease { LeaseId = leaseId });
        }

        [Fact]
        public async Task Can_acquire_lease()
        {
            var leaseId = Guid.NewGuid().ToString();
            var leaseStore = new InMemoryLeaseStore<MyLease>(new DefaultLeaseCreation<MyLease>());
            var leaseStored = await leaseStore.TryAcquire(leaseId);
            Assert.NotNull(leaseStored);
        }

        [Fact]
        public async Task Can_get_lease()
        {
            var leaseId = Guid.NewGuid().ToString();
            var originalLease = new MyLease {LeaseId = leaseId};

            var leaseStore = new InMemoryLeaseStore<MyLease>(new DefaultLeaseCreation<MyLease>());
            await leaseStore.Put(originalLease);

            var leaseStored = await leaseStore.TryAcquire(leaseId);
            Assert.NotNull(leaseStored);
            Assert.Equal(originalLease, leaseStored);
        }

        [Fact]
        public async Task Can_list_leases()
        {
            var leaseStore = new InMemoryLeaseStore<MyLease>(new DefaultLeaseCreation<MyLease>());
            await leaseStore.Put(new MyLease { LeaseId = Guid.NewGuid().ToString() });
            await leaseStore.Put(new MyLease { LeaseId = Guid.NewGuid().ToString() });
            await leaseStore.Put(new MyLease { LeaseId = Guid.NewGuid().ToString() });
            await leaseStore.Put(new MyLease { LeaseId = Guid.NewGuid().ToString() });

            var stored = await leaseStore.List();
            Assert.Equal(4, stored.Count());
        }

        public class MyLease : ILease
        {
            public string LeaseId { get; set; }
            public object State { get; set; }
        }
    }
}