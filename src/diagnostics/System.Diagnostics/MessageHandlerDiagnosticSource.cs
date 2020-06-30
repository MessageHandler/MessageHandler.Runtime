using System;
using System.Diagnostics;

namespace MessageHandler.Runtime
{    public class MessageHandlerDiagnosticSource
    {
        public const string DiagnosticListenerName = "MessageHandler.Runtime";
        public const string BaseActivityName = DiagnosticListenerName + ".";

        private static readonly DiagnosticListener _diagnosticListener = new DiagnosticListener(DiagnosticListenerName);

        public Activity Start(string operationName, Activity parent, Func<ActivityInfo> getPayload)
        {
            string activityName = BaseActivityName + operationName;
            var activity = new Activity(activityName);

            if(parent != null)
            {
                var parentSpanId = parent.SpanId.ToHexString() != "0000000000000000" ? parent.SpanId : parent.ParentSpanId;
                activity.SetParentId(parent.TraceId, parentSpanId, parent.ActivityTraceFlags);
            }

            if (_diagnosticListener.IsEnabled(activityName))
            {
                _diagnosticListener.StartActivity(activity, getPayload());
            }
            else
            {
                activity.Start();
            }

            return activity;
        }

        public void Stop(Activity activity, Func<ActivityInfo> getPayload)
        {
            if (activity != null)
            {
                if (_diagnosticListener.IsEnabled(activity.OperationName))
                {
                    _diagnosticListener.StopActivity(activity, getPayload());
                }
                else
                {
                    activity.Stop();
                }
            }
        }
    }

    public class ActivityInfo
    {
        public string Name { get; set; }
        public string Command { get; set; }
    }
}
