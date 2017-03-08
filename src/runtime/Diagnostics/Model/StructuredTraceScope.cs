using System;

namespace MessageHandler.Runtime
{
    [Flags]
    public enum StructuredTraceScope
    {
        Infrastructure = 1,
        Domain = 2
    }
}