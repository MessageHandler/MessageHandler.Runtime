using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;

namespace MessageHandler.EventProcessing.Runtime
{
    public class HandlerRuntime
    {
        private HandlerRuntimeConfiguration config;

        public HandlerRuntime(HandlerRuntimeConfiguration config)
        {
            this.config = config;
        }

        private HandlerRuntime()
        {
            
        }
        public static HandlerRuntime Create(HandlerRuntimeConfiguration config)
        {
            config.LockSettings();
            var runtime = new HandlerRuntime(config);

            return runtime;
        }
    }
}