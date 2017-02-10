using System;

namespace MessageHandler.EventProcessing.Runtime
{
    public class StartupTaskRegisteredException : Exception
    {
        public StartupTaskRegisteredException()
        {
        }
        public StartupTaskRegisteredException(string message) : base(message)
        {
        }
    }
}