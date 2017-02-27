namespace MessageHandler.Runtime
{
    public interface ILease
    {
        string LeaseId { get; set; }
        object State { get; set; }
    }
}