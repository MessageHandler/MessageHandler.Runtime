using System;

namespace MessageHandler.Runtime.Diagnostics
{
    public class TraceSinkRegistration
    {
        public Type Type { get; set; }
        public StructuredTraceScope Scope { get; set; }
        public StructuredTraceSeverity Severity { get; set; }
    }
}