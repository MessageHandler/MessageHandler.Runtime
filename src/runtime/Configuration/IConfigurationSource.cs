namespace MessageHandler.EventProcessing.Runtime
{
    public interface IConfigurationSource
    {
        T GetConfiguration<T>() where T : class, new();
        T GetConfiguration<T>(string filename) where T : class, new();
    }
}
