namespace System.Web.Script.Serialization
{
    using System;
    using System.Web;

    public abstract class JavaScriptTypeResolver
    {
        public abstract Type ResolveType(string id);
        public abstract string ResolveTypeId(Type type);
    }
}