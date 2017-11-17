using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MessageHandler.Runtime.Serialization;

namespace MessageHandler.Runtime.Configuration
{
    public class JSonFileVariableSource : IVariableSource
    {
        private readonly string _filename = "variables.config.json";
        private readonly IList<Variable> _variables;

        public JSonFileVariableSource()
        {
            _variables = LoadVariables();
        }

        public JSonFileVariableSource(string filename)
        {
            _filename = filename;
            _variables = LoadVariables();
        }
       
        public dynamic GetVariables(string scopeType, string scopeId)
        {
            return new DynamicVariables(scopeType, _variables);
        }

        private IList<Variable> LoadVariables()
        {
            using (var reader = File.OpenText(_filename))
            {
                var json = reader.ReadToEnd();
                return Json.Decode<IList<Variable>>(json);
            }
        }

    }
}