using System;

namespace MessageHandler.Runtime
{
    public class InMemoryLeaseCreation : ICreateLeases<InMemoryLease>
    {
        public InMemoryLease Create(string leaseId)
        {
            return new InMemoryLease() { LeaseId = leaseId};
        }
    }
}