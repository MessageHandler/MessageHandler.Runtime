using System;

namespace MessageHandler.EventProcessing.Runtime.ConfigurationSettings
{
    public interface ISettings
    {
        void Set(string key, object value);
        void Set<T>(T value);
        T Get<T>();
        T Get<T>(string key);
        object Get(Type type);
        object Get(string key);
        void SetDefault<T>(T value);
        void SetDefault(string key, object value);
        object GetDefault(Type type);
        T GetDefault<T>(string key);
        object GetDefault(string key);
        T GetDefault<T>();
        void Remove<T>();
        void Remove(Type type);
        void Remove(string key);
        void Lock(string key);
        void LockAll();
        void Clear();
    }
}