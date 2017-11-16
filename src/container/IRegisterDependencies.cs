using System;

namespace MessageHandler.Runtime
{
    public interface IRegisterDependencies
    {
        void Register<T>();
        void Register<T>(Func<T> factory);
        void Register<T>(Lifecycle lifecycle);
        void Register<T>(Lifecycle lifecycle, Func<T> factory);
        void Register(Type type);
        void Register(Type type, Lifecycle lifecycle);
        void Register(Type type, Func<object> factory);
        void Register(Type type, Lifecycle lifecycle, Func<object> factory);
    }
}