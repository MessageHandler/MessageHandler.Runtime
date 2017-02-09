namespace MessageHandler.EventProcessing.Runtime
{
    public class HandlerRuntimeConfigurationValues
    {
        public string HandlerInstanceId { get; internal set; }
        public string HandlerConfigurationId { get; internal set; }
        public string AccountId { get; internal set; }
        public string EnvironmentId { get; internal set; }
        public string ChannelId { get; internal set; }
        public string TransportType { get; internal set; }
        public string Connectionstring { get; internal set; }
    }
}