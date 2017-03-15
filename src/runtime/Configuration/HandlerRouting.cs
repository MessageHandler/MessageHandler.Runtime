using System.Collections.Generic;

namespace MessageHandler.Runtime
{
    public class HandlerRouting
    {
        public IList<InputSubjectFilter> InputSubjectFilters { get; set; } = new List<InputSubjectFilter>();
        public IDictionary<string, IList<OutputSubjectRoute>> OutputSubjectRoutes { get; set; } = new Dictionary<string, IList<OutputSubjectRoute>>();
        public IList<GatewayPartition> GatewayPartitions { get; set; } = new List<GatewayPartition>();
    }
}