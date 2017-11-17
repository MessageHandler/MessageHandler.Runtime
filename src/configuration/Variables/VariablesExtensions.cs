using System.Collections.Generic;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public static class VariablesExtensions
    {
        private const string handlerConfigurationVariablesKey = "messagehandler.uservariables";

        public static void UserVariables(this ConfigurationRoot configuration, List<Variable> variables)
        {
            var settings = configuration.GetSettings();
            settings.Set(handlerConfigurationVariablesKey, variables);
        }

        public static List<Variable> GetUserVariables(this ISettings settings)
        {
            return settings.GetOrCreate<List<Variable>>(handlerConfigurationVariablesKey);
        }
    }
}