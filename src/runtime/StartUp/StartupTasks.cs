using System;
using System.Collections.Generic;

namespace MessageHandler.EventProcessing.Runtime
{
    public class StartupTasks: List<Func<IStartupTask>>
    {}
}