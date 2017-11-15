using System;
using System.Runtime.Serialization;

namespace MessageHandler.Runtime.ConfigurationSettings
{
    public class SettingLockedException : Exception
    {
        public SettingLockedException()
        {
        }

        public SettingLockedException(string message) : base(message)
        {
        }

        public SettingLockedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SettingLockedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}