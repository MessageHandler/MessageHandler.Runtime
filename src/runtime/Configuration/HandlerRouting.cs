using System.Collections.Generic;

namespace MessageHandler.Runtime
{
    public class HandlerRouting
    {
        public IList<InputSubjectFilter> InputSubjectFilters { get; set; } = new List<InputSubjectFilter>();
    }
}