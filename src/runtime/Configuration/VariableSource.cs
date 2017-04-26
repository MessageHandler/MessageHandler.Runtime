using System.Collections.Generic;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public class VariableSource : IVariableSource
    {
        private readonly IList<Variable> _variables;

        public VariableSource(ISettings settings)
        {
            _variables = settings.GetUserVariables();
        }

        public dynamic GetVariables(VariableScope scopeType, string scopeId)
        {
            return new DynamicVariable(scopeType.ToString(), _variables);
        }
    }
}