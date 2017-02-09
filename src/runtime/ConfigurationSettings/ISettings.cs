using System;

namespace MessageHandler.EventProcessing.Runtime.ConfigurationSettings
{
    public interface ISettings:IManageSettings,IProvideSettings
    {    }

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
    }

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