using System;
using System.Collections.Generic;

namespace MessageHandler.Runtime
{
    public interface IResolveDependencies
    {
        T Resolve<T>();
        object Resolve(Type type);

        IEnumerable<T> ResolveAll<T>();
        IEnumerable<object> ResolveAll(Type type);
    }
}