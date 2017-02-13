using System;
using System.Threading.Tasks;
using MessageHandler.Runtime;
using Xunit;

namespace unittests.ConfigurationConventions
{
    public class When_applying_conventions
    {
        [Fact]
        public async Task Can_register_conventions()
        {
            var config = new HandlerRuntimeConfiguration();
            var convention = new MyConvention();
            config.RegisterConvention(convention);
            await HandlerRuntime.Create(config).ConfigureAwait(false);
            Assert.True(convention.ApplyCalled);
        }
        public class MyConvention:IConvention
        {
            public bool ApplyCalled;
            public async Task Apply(HandlerRuntimeConfiguration configuration)
            {
                ApplyCalled = true;
                configuration.ShutdownGracePeriod(TimeSpan.FromSeconds(1)); //Proves that lock is not yet called
            }
        }
    }
}
