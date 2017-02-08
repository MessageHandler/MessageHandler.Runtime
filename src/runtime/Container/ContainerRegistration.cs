using System;

namespace MessageHandler.EventProcessing.Runtime
{
    internal class ContainerRegistration
    {
        public Type Type { get; set; }
        public Lifecycle Lifecycle { get; set; }
        public Func<object> Factory { get; set; }
        public object SingletonInstance { get; set; }

    }
}
