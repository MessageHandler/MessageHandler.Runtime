using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CSharp.RuntimeBinder;

namespace MessageHandler.Runtime
{
    public class RoslynScriptingEngine : IScriptEngine
    {
        private readonly ConcurrentDictionary<string, Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, object>> _compiled = new ConcurrentDictionary<string, Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, object>>(); 

        public string Execute(string script, dynamic message = null, dynamic channel = null, dynamic environment = null, dynamic account = null, dynamic context = null)
        {
            var func = _compiled.GetOrAdd(script, s =>
            {
                var assembly = Compile(string.Format(ExpressionCode, script));
                var type = assembly.GetType("CompiledExpression");
                var method = type.GetMethod("Run");

                return (Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, object>)method.CreateDelegate(typeof(Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, object>));
            });

            var result = func(message, channel, environment, account, null, context);
            return result?.ToString() ?? "";
        }

        public string Execute(string script, Dictionary<ScriptScope, object> parameters)
        {
            dynamic message, channel, environment, account, project, context;
            parameters.TryGetValue(ScriptScope.Message, out message);
            parameters.TryGetValue(ScriptScope.Channel, out channel);
            parameters.TryGetValue(ScriptScope.Environment, out environment);
            parameters.TryGetValue(ScriptScope.Account, out account);
            parameters.TryGetValue(ScriptScope.Project, out project);
            parameters.TryGetValue(ScriptScope.Context, out context);

            var func = _compiled.GetOrAdd(script, s =>
            {
                var assembly = Compile(string.Format(ExpressionCode, script));
                var type = assembly.GetType("CompiledExpression");
                var method = type.GetMethod("Run");

                return (Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, object>)method.CreateDelegate(typeof(Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, object>));
            });

            var result = func(message, channel, environment, account, project, context);
            return result?.ToString() ?? "";
        }

        private Assembly Compile(params string[] sources)
        {
            var assemblyFileName = "gen" + Guid.NewGuid().ToString().Replace("-", "") + ".dll";
            var compilation = CSharpCompilation.Create(assemblyFileName,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
                syntaxTrees: from source in sources
                             select CSharpSyntaxTree.ParseText(source),
                references: new[]
                {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                     MetadataReference.CreateFromFile(typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo).Assembly.Location),
                     MetadataReference.CreateFromFile(typeof(System.Runtime.CompilerServices.DynamicAttribute).Assembly.Location)
                 });

            EmitResult emitResult;

            using (var ms = new MemoryStream())
            {
                emitResult = compilation.Emit(ms);

                if (emitResult.Success)
                {
                    var assembly = Assembly.Load(ms.GetBuffer());
                    return assembly;
                }
            }

            var message = string.Join("\r\n", emitResult.Diagnostics);
            throw new ApplicationException(message);
        }

        const string ExpressionCode =   "using System;" +
                                        "public class CompiledExpression" +
                                        "{{" +
                                            "public static object Run(dynamic m, dynamic c, dynamic e, dynamic a, dynamic p, dynamic cx)" +
                                            "{{" +
                                                "Func<dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, dynamic, object> func = (message, msg, channel, environment, account, project, context) => {0};" +
                                                "return func(m, m, c, e, a, p, cx);" +
                                            "}}" +
                                        "}}";

        
    }
}