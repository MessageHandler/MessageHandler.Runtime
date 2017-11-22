using System;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class ShutdownExtensions
    {
        private const string ShutdownGracePeriodKey = "messagehandler.shutdowngraceperiod";
        public static void ShutdownGracePeriod(this ConfigurationRoot configuration, TimeSpan gracePeriod)
        {
            var settings = configuration.GetSettings();
            settings.Set(ShutdownGracePeriodKey, gracePeriod);
        }

        public static TimeSpan GetShutdownGracePeriod(this ISettings settings)
        {
            return settings.Get<TimeSpan>(ShutdownGracePeriodKey);
        }

        public static void SetDefaultShutdownGracePeriod(this ISettings settings, TimeSpan gracePeriod)
        {
            settings.SetDefault(ShutdownGracePeriodKey, gracePeriod);
        }
    }
}