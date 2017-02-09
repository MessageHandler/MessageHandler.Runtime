using System;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;

namespace MessageHandler.EventProcessing.Runtime
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