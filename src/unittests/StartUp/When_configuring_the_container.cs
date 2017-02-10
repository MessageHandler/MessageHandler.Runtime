using MessageHandler.EventProcessing.Runtime;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;
using Xunit;

namespace unittests.StartUp
{
    public class When_configuring_the_container
    {
        [Fact]
        public void Will_use_configured_container()
        {
            var container = new Container();
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.UseContainer(container);
            var runtime = HandlerRuntime.Create(config);

            Assert.StrictEqual(settings.Get("messagehandler.container"), container);
        }

        [Fact]
        public void Will_use_default_container_if_not_configured()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            var runtime = HandlerRuntime.Create(config);

            Assert.IsType<Container>(settings.Get("messagehandler.container"));
        }

        [Fact]
        public void Can_find_settings_in_container_when_setting_container()
        {
            var container = new Container();
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            config.UseContainer(container);
            var runtime = HandlerRuntime.Create(config);

            Assert.StrictEqual(settings, container.Resolve<ISettings>());
        }

        [Fact]
        public void Can_find_settings_in_container_when_not_setting_container()
        {
            var settings = new Settings();
            var config = new HandlerRuntimeConfiguration(settings);
            var runtime = HandlerRuntime.Create(config);
            var container = settings.Get<IContainer>("messagehandler.container");

            Assert.StrictEqual(settings, container.Resolve<ISettings>());
        }

        [Fact]
        public void Can_find_dependencies_resolver_in_container()
        {
            var container = new Container();
            Assert.NotNull(container.Resolve<IResolveDependencies>());
        }

        [Fact]
        public void Can_find_dependency_registration()
        {
            var container = new Container();
            Assert.NotNull(container.Resolve<IRegisterDependencies>());
        }

    }
}