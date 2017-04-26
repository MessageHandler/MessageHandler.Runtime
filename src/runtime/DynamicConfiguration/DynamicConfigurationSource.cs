using System.Threading.Tasks;

namespace MessageHandler.Runtime.Configuration
{
    public class DynamicConfigurationSource : IDynamicConfigurationSource
    {
        private readonly ITemplateEngine _templateEngine;
        private readonly IConfigurationSource _configurationSource;

        public DynamicConfigurationSource(ITemplateEngine templateEngine, IConfigurationSource configurationSource)
        {
            _templateEngine = templateEngine;
            _configurationSource = configurationSource;
        }

        public async Task<T> GetConfiguration<T>() where T : class, new()
        {
            var config = await _configurationSource.GetConfiguration<T>();
            ApplyTemplates(config);
            return config;
        }

        public async Task<T> GetConfiguration<T>(string filename) where T : class, new()
        {
            var config = await _configurationSource.GetConfiguration<T>(filename);
            ApplyTemplates(config);
            return config;
        }

        private void ApplyTemplates<T>(T config)
        {
            var type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                var value = property.GetValue(config).ToString();
                var transformed = _templateEngine.Apply(value, null, null);
                property.SetValue(config, transformed);
            }
        }
    }
}