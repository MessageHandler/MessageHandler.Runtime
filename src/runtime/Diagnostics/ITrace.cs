using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace MessageHandler.Runtime
{
    public interface ITrace
    {
        StructuredTraceCompletionBehavior DefaultCompletionBehavior { get; set; }

        Task Start(CancellationToken token);
        Task Stop();

        Task Verbose(string text, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "");
        Task Debug(string text, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "");
        Task Info(string text, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "");
        Task Warn(string text, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "");
        Task Error(string text, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "");
        Task Error(string text, Exception exception, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "");
        Task Fatal(string text, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "");
        Task Fatal(string text, Exception exception, StructuredTraceScope scope = StructuredTraceScope.Domain, [CallerMemberName] string callerName = "");

        Task Add(StructuredTrace traced);
        Task Add(StructuredTrace traced, StructuredTraceCompletionBehavior completionBehavior);
    }
}