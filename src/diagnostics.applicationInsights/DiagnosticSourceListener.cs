using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Common;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace MessageHandler.Runtime
{
    public class DiagnosticSourceListener : IObserver<DiagnosticListener>, IDiagnosticEventHandler, IDisposable
    {
        private TelemetryClient _client;
        
        private static readonly ConcurrentDictionary<string, ActiveSubsciptionManager> SubscriptionManagers = new ConcurrentDictionary<string, ActiveSubsciptionManager>();
        private readonly ConcurrentQueue<IDisposable> individualSubscriptions = new ConcurrentQueue<IDisposable>();
        private readonly ConcurrentQueue<IndividualDiagnosticSourceListener> individualListeners = new ConcurrentQueue<IndividualDiagnosticSourceListener>();

        private readonly HashSet<string> includedDiagnosticSources = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, HashSet<string>> includedDiagnosticSourceActivities = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

        private IDisposable _subscription;

        public DiagnosticSourceListener(TelemetryClient client)
        {
            this._client = client;

            includedDiagnosticSources.Add(MessageHandlerDiagnosticSource.DiagnosticListenerName);
        }

        public void Subscribe()
        {
            if (this._subscription != null)
            {
                return;
            }

            this._subscription = DiagnosticListener.AllListeners.Subscribe(this);
        }

        bool IsSourceEnabled(DiagnosticListener value)
        {
            return this.includedDiagnosticSources.Contains(value.Name);
        }

        bool IsActivityEnabled(string activityName, HashSet<string> includedActivities)
        {
            return includedActivities == null || includedActivities.Contains(activityName);
        }

        public void OnNext(DiagnosticListener value)
        {
            if (value == null || !this.IsSourceEnabled(value))
            {
                return;
            }

            var manager = SubscriptionManagers.GetOrAdd(value.Name, k => new ActiveSubsciptionManager());

            var individualListener = new IndividualDiagnosticSourceListener(
                value,
                this,
                this,
                this.GetListenerContext(value),
                manager);

            manager.Attach(individualListener);

            IDisposable subscription = value.Subscribe(
                individualListener,
                (evnt, input1, input2) => this.IsActivityEnabled(evnt, individualListener.context) && this.IsEventEnabled(evnt, input1, input2));

            this.individualSubscriptions.Enqueue(subscription);
            this.individualListeners.Enqueue(individualListener);
        }

        HashSet<string> GetListenerContext(DiagnosticListener diagnosticListener)
        {
            if (!this.includedDiagnosticSourceActivities.TryGetValue(diagnosticListener.Name, out var includedActivities))
            {
                return null;
            }

            return includedActivities;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                while (this.individualListeners.TryDequeue(out var individualListener))
                {
                    individualListener.Dispose();
                }

                while (this.individualSubscriptions.TryDequeue(out var individualSubscription))
                {
                    individualSubscription.Dispose();
                }

                this._subscription?.Dispose();
            }
        }

        public bool IsEventEnabled(string evnt, object arg1, object arg2)
        {
            return true;
        }

        public void OnEvent(KeyValuePair<string, object> evnt, DiagnosticListener diagnosticListener)
        {
            if (evnt.Key.EndsWith(".Start"))
            {
                Activity currentActivity = Activity.Current;
                ActivityInfo info = evnt.Value as ActivityInfo;

                var telemetry = ExtractDependencyTelemetry(currentActivity, info);
                if (telemetry == null)
                {
                    return;
                }

                telemetry.Context.Operation.Id = currentActivity.RootId;
                telemetry.Context.Operation.ParentId = currentActivity.ParentSpanId.ToHexString();
                telemetry.Timestamp = currentActivity.StartTimeUtc;

                telemetry.Properties["DiagnosticSource"] = diagnosticListener.Name;
                //telemetry.Properties["Activity"] = currentActivity.OperationName;

                this._client.TrackDependency(telemetry);
            }
        }

        internal DependencyTelemetry ExtractDependencyTelemetry(Activity currentActivity, ActivityInfo info)
        {
            DependencyTelemetry telemetry = new DependencyTelemetry
            {
                Id = currentActivity.SpanId.ToHexString(),
                Duration = currentActivity.Duration,
                Name = info?.Name ?? currentActivity.OperationName,
                Type = info?.Type ?? "MessageHandler",
                Data = info?.Command
            };

            foreach (KeyValuePair<string, string> tag in currentActivity.Tags)
            {
                if (!telemetry.Properties.ContainsKey(tag.Key))
                {
                    telemetry.Properties.Add(tag);
                }
            }

            return telemetry;
        }        

        internal class IndividualDiagnosticSourceListener : IObserver<KeyValuePair<string, object>>, IDisposable
        {
            private readonly DiagnosticListener diagnosticListener;
            private readonly IDiagnosticEventHandler eventHandler;
            private readonly DiagnosticSourceListener telemetryDiagnosticSourceListener;
            private readonly ActiveSubsciptionManager subscriptionManager;
            internal readonly HashSet<string> context;

            internal IndividualDiagnosticSourceListener(
                DiagnosticListener diagnosticListener,
                IDiagnosticEventHandler eventHandler,
                DiagnosticSourceListener telemetryDiagnosticSourceListener,
                HashSet<string> context,
                ActiveSubsciptionManager subscriptionManager)
            {
                this.diagnosticListener = diagnosticListener;
                this.eventHandler = eventHandler;
                this.telemetryDiagnosticSourceListener = telemetryDiagnosticSourceListener;
                this.context = context;
                this.subscriptionManager = subscriptionManager;
                this.subscriptionManager.Attach(this);
            }

            public void OnCompleted()
            {
            }

            public void OnError(Exception error)
            {
            }

            public void OnNext(KeyValuePair<string, object> evnt)
            {
                // It's possible to host multiple apps (ASP.NET Core or generic hosts) in the same process
                // Each of this apps has it's own DependencyTrackingModule and corresponding listener for specific source.
                // We should ignore events for all of them except one
                if (!this.subscriptionManager.IsActive(this))
                {
                    return;
                }

                // while we provide IsEnabled callback during subscription, it does not gurantee events will not be fired
                // In case of multiple subscribers, it's enough for one to reply true to IsEnabled.
                // I.e. check for if activity is not disabled and particular handler wants to receive the event.
                if (this.telemetryDiagnosticSourceListener.IsActivityEnabled(evnt.Key, this.context) && this.eventHandler.IsEventEnabled(evnt.Key, null, null))
                {
                    Activity currentActivity = Activity.Current;
                    if (currentActivity == null)
                    {
                        return;
                    }

                    this.eventHandler.OnEvent(evnt, this.diagnosticListener);
                }
            }

            public void Dispose()
            {
                this.subscriptionManager?.Detach(this);
            }
        }
    }

    internal interface IDiagnosticEventHandler
    {
        void OnEvent(KeyValuePair<string, object> evnt, DiagnosticListener diagnosticListener);

        bool IsEventEnabled(string evnt, object arg1, object arg2);
    }
}
