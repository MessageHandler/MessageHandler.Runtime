using System;
using System.Diagnostics;

namespace MessageHandler.Runtime
{
    public class StructuredTrace
    {
        public string Text { get; set; }
        public object State { get; set; }
        public string What { get; set; }
        public string Where { get; set; }
        public string Who { get; set; }
        public DateTimeOffset When { get; set; } = DateTimeOffset.UtcNow;
        public StructuredTraceSeverity Severity { get; set; } = StructuredTraceSeverity.Verbose;
        public StructuredTraceScope Scope { get; set; } = StructuredTraceScope.Domain;
        public Activity ActivityTrace { get; set; }
    }
}