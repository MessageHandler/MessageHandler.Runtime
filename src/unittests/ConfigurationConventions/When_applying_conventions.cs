using System;
using MessageHandler.EventProcessing.Runtime;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;
using Xunit;

namespace unittests.ConfigurationConventions
{
    public class When_applying_conventions
    {
        [Fact]
        public void Can_register_conventions()
        {
            var config = new HandlerRuntimeConfiguration();
            var convention = new MyConvention();
            config.RegisterConvention(convention);
            HandlerRuntime.Create(config);
            Assert.True(convention.ApplyCalled);
        }
        public class MyConvention:IConvention
        {
            public bool ApplyCalled;
            public void Apply(HandlerRuntimeConfiguration configuration)
            {
                ApplyCalled = true;
                configuration.ShutdownGracePeriod(TimeSpan.FromSeconds(1)); //Proves that lock is not yet called
            }
        }
    }
}
