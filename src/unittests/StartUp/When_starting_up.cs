using System;
using MessageHandler.EventProcessing.Runtime;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;
using Xunit;

namespace unittests.StartUp
{
    public class When_starting_up
    {
        [Fact]
        public void Will_lock_settings_after_configuration()
        {
            var settings = new FakeSettings();

            var config = new HandlerRuntimeConfiguration(settings);
            var runtime = HandlerRuntime.Create(config);
            Assert.True(settings.LockIsCalled);
        }


        public class FakeSettings:ISettings
        {
            public bool LockIsCalled;
            public void Set(string key, object value)
            {
                throw new NotImplementedException();
            }

            public void Set<T>(T value)
            {
                throw new NotImplementedException();
            }

            public T Get<T>()
            {
                throw new NotImplementedException();
            }

            public T Get<T>(string key)
            {
                throw new NotImplementedException();
            }

            public object Get(Type type)
            {
                throw new NotImplementedException();
            }

            public object Get(string key)
            {
                throw new NotImplementedException();
            }

            public void SetDefault<T>(T value)
            {
                throw new NotImplementedException();
            }

            public void SetDefault(string key, object value)
            {
                throw new NotImplementedException();
            }

            public object GetDefault(Type type)
            {
                throw new NotImplementedException();
            }

            public T GetDefault<T>(string key)
            {
                throw new NotImplementedException();
            }

            public object GetDefault(string key)
            {
                throw new NotImplementedException();
            }

            public T GetDefault<T>()
            {
                throw new NotImplementedException();
            }

            public void Remove<T>()
            {
                throw new NotImplementedException();
            }

            public void Remove(Type type)
            {
                throw new NotImplementedException();
            }

            public void Remove(string key)
            {
                throw new NotImplementedException();
            }

            public void Lock(string key)
            {
                throw new NotImplementedException();
            }

            public void LockAll()
            {
                LockIsCalled = true;
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }
        }
    }
}
