using System.Collections.Generic;

namespace MessageHandler.Runtime
{
    public interface ITemplateEngine
    {
        string Apply(string template, object message, object context);

        string Apply(string template, Dictionary<ScriptScope, object> parameters);
    }
}