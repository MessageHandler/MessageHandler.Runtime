using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MessageHandler.EventProcessing.Runtime.ConfigurationSettings
{
    public class Settings
    {
        private readonly ConcurrentDictionary<string, Setting> _explicitValues = new ConcurrentDictionary<string, Setting>();
        private readonly ConcurrentDictionary<string, Setting> _defaultValues = new ConcurrentDictionary<string, Setting>();
        public void Set(string key, object value)
        {
            Setting setting;
            _explicitValues.TryGetValue(key, out setting);
            if (setting != null && setting.Locked)
            {
                throw new SettingLockedException($"Setting {key} is locked and cannot be changed.");
            }
            _explicitValues.AddOrUpdate(key, s => new Setting { Value = value }, (s, o) => new Setting { Value = value });


        }

        public object Get(string key)
        {
            Setting setting;
            if (_explicitValues.ContainsKey(key))
            {
                _explicitValues.TryGetValue(key, out setting);
            }
            else
            {
                _defaultValues.TryGetValue(key, out setting);
            }
            return setting?.Value;
        }

        public void SetDefault(string key, object value)
        {
            Setting setting;
            _defaultValues.TryGetValue(key, out setting);
            if (setting != null && setting.Locked)
            {
                throw new SettingLockedException($"Setting {key} is locked and cannot be changed.");
            }
            _defaultValues.AddOrUpdate(key, s => new Setting {Value = value}, (s, o) => new Setting {Value = value});
        }

        public object GetDefault(string key)
        {
            Setting setting;
            _defaultValues.TryGetValue(key, out setting);
            return setting?.Value;
        }

        public void Lock(string key)
        {
            Setting setting;
            _explicitValues.TryGetValue(key, out setting);
            if (setting != null)
            {
                setting.Locked = true;
            }
            _defaultValues.TryGetValue(key, out setting);
            if (setting != null)
            {
                setting.Locked= true;
            }

        }

        public void LockAll()
        {
            Setting setting;
            foreach (var key in _explicitValues.Keys)
            {
                _explicitValues.TryGetValue(key, out setting);
                if (setting != null)
                {
                    setting.Locked = true;
                }
            }
            foreach (var key in _defaultValues.Keys)
            {
                _defaultValues.TryGetValue(key, out setting);
                if (setting != null)
                {
                    setting.Locked = true;
                }
            }
        }

        public void Remove(string key)
        {
            Setting setting;
            _explicitValues.TryRemove(key, out setting);
            _defaultValues.TryRemove(key, out setting);
        }

        public void Clear()
        {
            _explicitValues.Clear();
            _defaultValues.Clear();
        }
    }

    internal class Setting
    {
        public object Value { get; set; }
        public bool Locked { get; set; }
    }
}