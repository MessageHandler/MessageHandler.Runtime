using System.Collections.Generic;

namespace MessageHandler.Runtime
{
    public interface IScriptEngine
    {
        string Execute(string script, Dictionary<ScriptScope, object> parameters);
    }
}