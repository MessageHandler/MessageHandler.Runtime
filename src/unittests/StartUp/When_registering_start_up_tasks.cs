using System.Threading.Tasks;
using MessageHandler.EventProcessing.Runtime;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;
using Xunit;

namespace unittests.StartUp
{
    public class When_registering_startup_tasks
    {
        [Fact]
        public void Can_register_startup_task_instance_and_find_it_in_the_container()
        {
            var startupTask = new MyStartupTask();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterStartupTask(startupTask);
            Assert.NotNull(container.Resolve<IStartupTask>());
            Assert.IsType<MyStartupTask>(container.Resolve<IStartupTask>());
        }

        [Fact]
        public void Can_register_startup_task_instance_and_find_its_type_in_the_settings()
        {
            var settings = new Settings();
            var startupTask = new MyStartupTask();
            var configuration = new HandlerRuntimeConfiguration(settings);
            configuration.RegisterStartupTask(startupTask);
            var startupTasks = settings.Get<StartupTaskTypes>();
            Assert.NotNull(startupTasks.Exists(t => t == typeof(MyStartupTask)));
        }

        [Fact]
        public async Task Will_run_startup_task()
        {
            var startupTask = new MyStartupTask();
            var configuration = new HandlerRuntimeConfiguration();
            configuration.RegisterStartupTask(startupTask);
            var runtime = await HandlerRuntime.Create(configuration);
            await runtime.Start();
            Assert.True(startupTask.RunIsCalled);
        }

        [Fact]
        public void Can_register_startup_task_instance_and_find_it_in_the_container_generic()
        {
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterStartupTask<MyStartupTask>();
            Assert.NotNull(container.Resolve<IStartupTask>());
            Assert.IsType<MyStartupTask>(container.Resolve<IStartupTask>());
        }

        [Fact]
        public void Can_register_startup_task_instance_and_find_it_in_the_container_by_type()
        {
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterStartupTask(typeof(MyStartupTask));
            Assert.NotNull(container.Resolve<IStartupTask>());
            Assert.IsType<MyStartupTask>(container.Resolve<IStartupTask>());
        }

        [Fact]
        public async Task Can_OR_NOT_register_startup_task_instance_multiple_times()
        {
            var startupTask = new MyStartupTask();
            var configuration = new HandlerRuntimeConfiguration();
            configuration.RegisterStartupTask(startupTask);
            var runtime = await HandlerRuntime.Create(configuration);
            await runtime.Start();
            Assert.Throws<StartupTaskRegisteredException>(() => configuration.RegisterStartupTask(startupTask));
        }

        public class MyStartupTask:IStartupTask
        {
            public bool RunIsCalled;
            public async Task Run()
            {
                RunIsCalled = !RunIsCalled;
            }
        }
    }
}
