using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
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