using System;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class LeaseTypeSettingsExtensions
    {
        public static void SetLeaseType(this ISettings settings, Type leaseType)
        {
            settings.Set("LeaseType", leaseType);
        }

        public static void SetLeaseStoreType(this ISettings settings, Type leaseType)
        {
            settings.Set("LeaseStoreType", leaseType);
        }

        public static Type GetLeaseType(this ISettings settings)
        {
            return settings.Get<Type>("LeaseType");
        }

        public static Type GetLeaseStoreType(this ISettings settings)
        {
            return settings.Get<Type>("LeaseStoreType");
        }
    }
}