using System;
using System.Threading.Tasks;
using MessageHandler.EventProcessing.Runtime;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;
using Xunit;

namespace unittests.StartUp
{
    public class When_starting_up
    {
        [Fact]
        public async Task Will_lock_settings_after_configuration()
        {
            var settings = new FakeSettings();

            var config = new HandlerRuntimeConfiguration(settings);
            var runtime = await HandlerRuntime.Create(config);
            Assert.True(settings.LockIsCalled);
        }


        public class FakeSettings:ISettings
        {
            public bool LockIsCalled;
            public void Set(string key, object value)
            {
            }

            public void Set<T>(T value)
            {
            }

            public T Get<T>()
            {
                return default(T);
            }

            public T Get<T>(string key)
            {
                return default(T);
            }

            public object Get(Type type)
            {
                return null;
            }

            public object Get(string key)
            {
                return null;
            }

            public void SetDefault<T>(T value)
            {
                
            }

            public void SetDefault(string key, object value)
            {
               
            }

            public object GetDefault(Type type)
            {
                return null;
            }

            public T GetDefault<T>(string key)
            {
                return default(T);
            }

            public object GetDefault(string key)
            {
                return null;
            }

            public T GetDefault<T>()
            {
                return default(T);
            }

            public void Remove<T>()
            {
            }

            public void Remove(Type type)
            {
            }

            public void Remove(string key)
            {
            }

            public void Lock(string key)
            {
            }

            public void LockAll()
            {
                LockIsCalled = true;
            }

            public void Clear()
            {
            }

            public T GetOrCreate<T>()
            {
                return Activator.CreateInstance<T>();
            }

            public object GetOrCreate(Type type)
            {
                return Activator.CreateInstance(type);
            }

            public object GetOrCreate(string key, Type type)
            {
                return Activator.CreateInstance(type);
            }
        }
    }
}
