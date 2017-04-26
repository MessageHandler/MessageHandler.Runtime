using System.Collections.Generic;
using System.Text.RegularExpressions;
using MessageHandler.Runtime.ConfigurationSettings;

namespace MessageHandler.Runtime
{
    public class RoslynRegexTemplatingEngine : ITemplateEngine
    {
        private readonly RoslynScriptingEngine _engine;
        private readonly Regex _rex = new Regex("@(msg|message|channel|environment|account|project|context)((\\.)([a-zA-Z0-9_])+(\\((.*?)\\))*)+");
        private readonly IVariableSource variableSource;
        private readonly ISettings _settings;


        public RoslynRegexTemplatingEngine(RoslynScriptingEngine engine, IVariableSource variableSource, ISettings settings)
        {
            this.variableSource = variableSource;
            _settings = settings;
            _engine = engine;
        }

        public string Apply(string template, object message = null, object channel = null, object environment = null, object account = null, object context = null)
        {
            if (string.IsNullOrEmpty(template)) return template;

            return (_rex.Replace(template, delegate(Match m)
            {
                string script = m.Value.Remove(0, 1);
                object rep = _engine.Execute(script, message, channel, environment, account, context);
                return (rep.ToString());
            }));
        }

        public string Apply(string template, object message, object context)
        {
            if (string.IsNullOrEmpty(template)) return template;

            var channel = variableSource.GetVariables(VariableScope.Channel, _settings.GetChannelId());
            var environment = variableSource.GetVariables(VariableScope.Environment, _settings.GetEnvironmentId());
            var account = variableSource.GetVariables(VariableScope.Account, _settings.GetAccountId());
            var project = variableSource.GetVariables(VariableScope.Project, _settings.GetProjectId());
            
            var parameters = new Dictionary<ScriptScope, object>
            {
                { ScriptScope.Message, message } ,
                { ScriptScope.Channel, channel } ,
                { ScriptScope.Environment, environment } ,
                { ScriptScope.Account, account } ,
                { ScriptScope.Project, project } ,
                { ScriptScope.Context, context }
            };

            return Apply(template, parameters);
        }

        public string Apply(string template, Dictionary<ScriptScope, object> parameters)
        {
            if (string.IsNullOrEmpty(template)) return template;

            return (_rex.Replace(template, delegate (Match m)
            {
                string script = m.Value.Remove(0, 1);
                object rep = _engine.Execute(script, parameters);
                return (rep.ToString());
            }));
        }
    }
    
}