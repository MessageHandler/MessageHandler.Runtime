using System;
using System.Threading;
using System.Threading.Tasks;
using MessageHandler.EventProcessing.Runtime;
using Xunit;

namespace unittests.Running
{
    public class When_executing_background_tasks
    {
        [Fact]
        public async Task Will_call_start_and_stop()
        {
            var backgroundTask = new MyBackgroundTask();
            var configuration = new HandlerRuntimeConfiguration();
            configuration.RegisterBackgroundTask(backgroundTask);
            var runtime = await HandlerRuntime.Create(configuration);
            await runtime.Start();
            await runtime.Stop();
            Assert.True(backgroundTask.StartCalled);
            Assert.True(backgroundTask.StopCalled);
        }

        [Fact]
        public async Task Will_call_start_on_seperate_thread()
        {
            var backgroundTask = new MyBackgroundTask();
            var configuration = new HandlerRuntimeConfiguration();
            configuration.RegisterBackgroundTask(backgroundTask);
            var runtime = await HandlerRuntime.Create(configuration);
            await runtime.Start();
            await runtime.Stop();
            Assert.NotEqual(Thread.CurrentThread.ManagedThreadId, backgroundTask.ManagedThreadId);
        }

        [Fact]
        public async Task Will_stop_even_if_background_task_cant_stop_in_2_seconds()
        {
            var timestamp = DateTime.UtcNow;
            var sleepTime = TimeSpan.FromSeconds(60);

            var backgroundTask = new MyBackgroundTask(sleepTime);
            var configuration = new HandlerRuntimeConfiguration();
            configuration.RegisterBackgroundTask(backgroundTask);
            configuration.ShutdownGracePeriod(TimeSpan.FromSeconds(2));
            var runtime = await HandlerRuntime.Create(configuration);
            await runtime.Start();
            await Task.Delay(TimeSpan.FromMilliseconds(500));
            await runtime.Stop();

            var executionTime = DateTime.UtcNow - timestamp;

            Assert.True(backgroundTask.StartCalled);
            Assert.False(backgroundTask.StopCalled);
            Assert.True(executionTime < sleepTime);
        }

        public class MyBackgroundTask : IBackgroundTask
        {
            private readonly TimeSpan _sleepTime;
            public bool StartCalled;
            public bool StopCalled;
            public int ManagedThreadId;

            public MyBackgroundTask()
            {
                _sleepTime = TimeSpan.FromSeconds(1);
            }
            public MyBackgroundTask(TimeSpan sleepTime)
            {
                _sleepTime = sleepTime;
            }

            public async Task Run(CancellationToken cancellation)
            {
                ManagedThreadId = Thread.CurrentThread.ManagedThreadId;
                StartCalled = true;

                while (!cancellation.IsCancellationRequested)
                {
                    await Task.Delay(_sleepTime);
                }

                StopCalled = true;
            }
        }
    }
}