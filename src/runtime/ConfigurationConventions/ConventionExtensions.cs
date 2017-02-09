using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;

namespace MessageHandler.EventProcessing.Runtime
{
    public static class ConventionExtensions
    {
        public static void RegisterConvention(this HandlerRuntimeConfiguration configuration, IConvention convention)
        {
            var settings = configuration.GetSettings();
            var conventions = settings.GetOrCreate<Conventions>();
            conventions.Add(convention);
        }
    }
}