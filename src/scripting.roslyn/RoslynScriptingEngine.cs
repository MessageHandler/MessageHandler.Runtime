using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
            var references = CollectReferences();
            references.Add(MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location));
            references.Add(MetadataReference.CreateFromFile(typeof(RuntimeBinderException).GetTypeInfo().Assembly.Location));
            var assemblyFileName = "gen" + Guid.NewGuid().ToString().Replace("-", "") + ".dll";
            var compilation = CSharpCompilation.Create(assemblyFileName,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary),
                syntaxTrees: from source in sources
                             select CSharpSyntaxTree.ParseText(source),
                references: references.ToArray());

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

        private static List<MetadataReference> CollectReferences()
        {
            // first, collect all assemblies
            var assemblies = new HashSet<Assembly>();

            Collect(Assembly.Load(new AssemblyName("netstandard")));

            //// add extra assemblies which are not part of netstandard.dll, for example:
            //Collect(typeof(Uri).Assembly);

            // second, build metadata references for these assemblies
            var result = new List<MetadataReference>(assemblies.Count);
            foreach (var assembly in assemblies)
            {
                result.Add(MetadataReference.CreateFromFile(assembly.Location));
            }

            return result;

            // helper local function - add assembly and its referenced assemblies
            void Collect(Assembly assembly)
            {
                if (!assemblies.Add(assembly))
                {
                    // already added
                    return;
                }

                var referencedAssemblyNames = assembly.GetReferencedAssemblies();

                foreach (var assemblyName in referencedAssemblyNames)
                {
                    var loadedAssembly = Assembly.Load(assemblyName);
                    assemblies.Add(loadedAssembly);
                }
            }
        }


    }
}