namespace MessageHandler.Runtime
{
    public class DefaultLeaseCreation<T> : ICreateLeases<T> where T : Lease, new()
    {
        public T Create(string leaseId)
        {
            return new T {LeaseId = leaseId};
        }
    }
}