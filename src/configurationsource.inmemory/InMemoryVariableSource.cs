using System.Collections.Generic;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public class InMemoryVariableSource : IVariableSource
    {
        private readonly IList<Variable> _variables;

        public InMemoryVariableSource(IList<Variable> variables)
        {
            _variables = variables;
        }

        public dynamic GetVariables(string scopeType, string scopeId)
        {
            return new DynamicVariables(scopeType, _variables);
        }
    }
}