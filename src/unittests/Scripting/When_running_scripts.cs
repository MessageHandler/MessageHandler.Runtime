using System.IO;
using System.Threading.Tasks;
using MessageHandler.Runtime;
using MessageHandler.Runtime.Configuration;
using Xunit;
using Xunit.Sdk;

namespace unittests.Configuration
{
    public class When_running_scripts
    {
        [Fact]
        public async Task Can_run_script()
        {
            var engine = new RoslynScriptingEngine();

            var result = engine.Execute("message.Value > 0", message: new TestMessage { Value = 1 });

            Assert.Equal("True", result);
        }

        public class TestMessage
        {
            public int Value {
                get;
                set;
            }


        }
    }
}