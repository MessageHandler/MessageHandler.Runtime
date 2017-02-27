using System;

namespace MessageHandler.Runtime
{
    public class DefaultLeaseCreation<T> : ICreateLeases<T> where T : ILease
    {
        public T Create(string leaseId)
        {
            var instance = (T)Activator.CreateInstance(typeof(T));
            instance.LeaseId = leaseId;
            return instance;
        }
    }
}