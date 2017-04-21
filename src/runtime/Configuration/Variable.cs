using System.Collections.Generic;

namespace MessageHandler.Runtime
{
    public class Variable
    {
        public string Account { get; set; }
        public string ScopeIdentifier { get; set; }
        public string ScopeType { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string FullName { get; set; }
    }

    public enum VariableScope
    {
        Account,
        Channel,
        Environment,
        Project,
    }

    public interface IVariableSource
    {
        object GetVariables(VariableScope scopeType, string scopeId);
    }

    public class VariableSource : IVariableSource
    {
        private readonly IList<Variable> _variables;

        public VariableSource(IList<Variable> variables)
        {
            _variables = variables;
        }

        public dynamic GetVariables(VariableScope scopeType, string scopeId)
        {
            return new DynamicVariable(scopeType.ToString(), _variables);
        }
    }
}