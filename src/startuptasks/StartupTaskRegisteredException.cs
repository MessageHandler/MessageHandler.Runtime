using System;

namespace MessageHandler.Runtime
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