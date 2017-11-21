namespace MessageHandler.Runtime
{
    public class ConfigurationContext
    {
        public string HandlerInstanceId { get; internal set; }
        public string HandlerConfigurationId { get; internal set; }
        public string AccountId { get; internal set; }
        public string EnvironmentId { get; internal set; }
        public string ChannelId { get; internal set; }
        public string ProjectId { get; internal set; }
    }
}