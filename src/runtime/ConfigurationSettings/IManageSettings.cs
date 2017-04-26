using System;

namespace MessageHandler.Runtime.ConfigurationSettings
{
    public interface IManageSettings
    {
        void Set(string key, object value);
        void Set<T>(T value);
        void SetDefault<T>(T value);
        void SetDefault(string key, object value);
        void Remove<T>();
        void Remove(Type type);
        void Remove(string key);
        void Lock(string key);
        void LockAll();
        void Clear();
    }
}