namespace MessageHandler.EventProcessing.Runtime
{
    public interface IConvention
    {
        void Apply(HandlerRuntimeConfiguration configuration);
    }
}