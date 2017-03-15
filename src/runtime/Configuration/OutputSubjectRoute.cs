namespace MessageHandler.Runtime
{
    public class OutputSubjectRoute
    {
        public string Subject { get; set; }
        public DestinationChannel Channel { get; set; }
        public DestinationEndpoint Endpoint { get; set; }
    }

    public interface RouteDestination
    {
        
    }

    public class DestinationChannel : RouteDestination
    {
        public string ChannelId { get; set; }
        public string EnvironmentId { get; set; }
        public string ChannelType { get; set; }
        public string ConnectionString { get; set; }
    }

    public class DestinationEndpoint : RouteDestination
    {
        public string EndpointId { get; set; }
        public string ConnectionString { get; set; }
    }
}