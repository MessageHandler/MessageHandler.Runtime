using System;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public abstract class NeedsLeaseManagementSettings : SettingsExtensionPoint
    {
        protected NeedsLeaseManagementSettings(ISettings settings) : base(settings)
        {
        }

        public Type LeaseType { get; set; }

        public Type LeaseStoreType { get; set; }

    }
}