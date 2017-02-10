using System;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class ShutdownExtensions
    {
        public static void ShutdownGracePeriod(this HandlerRuntimeConfiguration configuration, TimeSpan gracePeriod)
        {
            var settings = configuration.GetSettings();
            settings.Set("messagehandler.shutdowngraceperiod", gracePeriod);
        }
    }
}