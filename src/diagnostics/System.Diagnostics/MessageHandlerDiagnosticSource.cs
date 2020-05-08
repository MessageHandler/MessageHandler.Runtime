using System;
using System.Diagnostics;

namespace MessageHandler.Runtime
{    public class MessageHandlerDiagnosticSource
    {
        public const string DiagnosticListenerName = "MessageHandler.Runtime";
        public const string BaseActivityName = DiagnosticListenerName + ".";

        private static readonly DiagnosticListener _diagnosticListener = new DiagnosticListener(DiagnosticListenerName);

        public Activity Start(string operationName, Activity parent, Func<object> getPayload)
        {
            string activityName = BaseActivityName + operationName;
            var activity = new Activity(activityName);

            activity.SetParentId(parent.TraceId, parent.ParentSpanId, parent.ActivityTraceFlags);
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

        public void Stop(Activity activity, Func<object> getPayload)
        {
            if (activity != null)
            {
                _diagnosticListener.StopActivity(activity, getPayload());
            }
        }
    }
}
