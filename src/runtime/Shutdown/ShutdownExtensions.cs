using System;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class ShutdownExtensions
    {
        private const string ShutdowngraceperiodKey = "messagehandler.shutdowngraceperiod";
        public static void ShutdownGracePeriod(this HandlerRuntimeConfiguration configuration, TimeSpan gracePeriod)
        {
            var settings = configuration.GetSettings();
            settings.Set(ShutdowngraceperiodKey, gracePeriod);
        }

        public static TimeSpan GetShutdownGracePeriod(this ISettings settings)
        {
            return settings.Get<TimeSpan>(ShutdowngraceperiodKey);
        }

        public static void SetDefaultTimeSpan(this ISettings settings, TimeSpan TimeSpan)
        {
            settings.SetDefault(ShutdowngraceperiodKey, TimeSpan);
        }
    }
}