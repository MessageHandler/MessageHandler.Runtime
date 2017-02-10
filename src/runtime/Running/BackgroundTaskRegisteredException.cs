using System;

namespace MessageHandler.EventProcessing.Runtime
{
    public class BackgroundTaskRegisteredException : Exception
    {
        public BackgroundTaskRegisteredException()
        {
        }

        public BackgroundTaskRegisteredException(string message) : base(message)
        {
        }
    }
}