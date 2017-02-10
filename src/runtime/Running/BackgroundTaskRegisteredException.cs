using System;

namespace MessageHandler.Runtime
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