using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessageHandler.Runtime.Configuration;

namespace MessageHandler.Runtime
{
    public class InMemoryConfigurationSource<T>: IConfigurationSource
    {
        private readonly IDictionary<Type, object> _configurations;

        public InMemoryConfigurationSource(IDictionary<Type, object> configurations)
        {
            _configurations = configurations;
        }

        public async Task<T> GetConfiguration<T>() where T : class, new()
        {
            _configurations.TryGetValue(typeof(T), out var value);
            return value as T;
        }
    }
}