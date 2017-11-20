namespace MessageHandler.Runtime
{
    public class InMemoryLease : ILease
    {
        public string LeaseId { get; set; }
        public object State { get; set; }
    }
}