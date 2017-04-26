using System;

namespace MessageHandler.Runtime.ConfigurationSettings
{
    public interface IProvideSettings
    {
        T Get<T>();
        T Get<T>(string key);
        object Get(Type type);
        object Get(string key);
        object GetDefault(Type type);
        T GetDefault<T>(string key);
        object GetDefault(string key);
        T GetDefault<T>();
        T GetOrCreate<T>();
        T GetOrCreate<T>(string key);
        object GetOrCreate(Type type);
        object GetOrCreate(string key, Type type);
    }
}