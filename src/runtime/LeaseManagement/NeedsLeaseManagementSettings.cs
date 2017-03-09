using System;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public abstract class NeedsLeaseManagementSettings : SettingsExtensionPoint
    {
        protected NeedsLeaseManagementSettings(ISettings settings) : base(settings)
        {
        }

        public Func<string, ILease> LeaseFactory { get; set; }
    
    }
}