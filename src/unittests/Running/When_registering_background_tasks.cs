using System.Threading;
using System.Threading.Tasks;
using MessageHandler.EventProcessing.Runtime;
using MessageHandler.EventProcessing.Runtime.ConfigurationSettings;
using Xunit;

namespace unittests.Running
{
    public class When_registering_background_tasks
    {
        [Fact]
        public void Can_register_background_task_instance_and_find_it_in_the_container()
        {
            var backgroundTask = new MyBackgroundTask();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterBackgroundTask(backgroundTask);
            Assert.NotNull(container.Resolve<IBackgroundTask>());
            Assert.IsType<MyBackgroundTask>(container.Resolve<IBackgroundTask>());
        }

        [Fact]
        public void Can_register_background_task_instance_and_find_it_in_the_container_generic()
        {
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterBackgroundTask<MyBackgroundTask>();
            Assert.NotNull(container.Resolve<IBackgroundTask>());
            Assert.IsType<MyBackgroundTask>(container.Resolve<IBackgroundTask>());
        }

        [Fact]
        public void Can_register_background_task_instance_and_find_it_in_the_container_by_type()
        {
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterBackgroundTask(typeof(MyBackgroundTask));
            Assert.NotNull(container.Resolve<IBackgroundTask>());
            Assert.IsType<MyBackgroundTask>(container.Resolve<IBackgroundTask>());
        }

        [Fact]
        public void Can_register_startup_task_instance_and_find_its_type_in_the_settings()
        {
            var settings = new Settings();
            var backgroundTask = new MyBackgroundTask();
            var configuration = new HandlerRuntimeConfiguration(settings);
            configuration.RegisterBackgroundTask(backgroundTask);
            var backgroundTasks = settings.Get<BackgroundTaskTypes>();
            Assert.NotNull(backgroundTasks.Exists(t => t == typeof(MyBackgroundTask)));
        }

        [Fact]
        public void Can_OR_NOT_register_background_task_instance_multiple_types()
        {
            var backgroundTask = new MyBackgroundTask();
            var configuration = new HandlerRuntimeConfiguration();
            var container = new Container();
            configuration.UseContainer(container);
            configuration.RegisterBackgroundTask(backgroundTask);
            Assert.Throws<BackgroundTaskRegisteredException>(()=> configuration.RegisterBackgroundTask(backgroundTask));
        }


        public class MyBackgroundTask : IBackgroundTask
        {
            public bool StartCalled;

            public async Task Run(CancellationToken cancellation)
            {
                StartCalled = !StartCalled;
            }
        }
    }
}
