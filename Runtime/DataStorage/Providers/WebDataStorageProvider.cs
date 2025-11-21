#if UNITY_WEBGL
using System;
using System.Collections.Generic;
using System.Linq;
using Playgama;

namespace MewtonGames.DataStorage.Providers
{
    public class WebDataStorageProvider : IDataStorageProvider
    {
        public void Get(string key, Action<string> onComplete)
        {
            Bridge.storage.Get(key, (_, data) => { onComplete?.Invoke(data); });
        }

        public void Get(List<string> keys, Action<List<string>> onComplete)
        {
            Bridge.storage.Get(keys, (_, data) => { onComplete?.Invoke(data); });
        }

        public void Set(string key, string value, Action onComplete = null)
        {
            Bridge.storage.Set(key, value, _ => { onComplete?.Invoke(); });
        }

        public void Set(List<string> keys, List<string> values, Action onComplete = null)
        {
            Bridge.storage.Set(keys, values.ToList<object>(), _ => { onComplete?.Invoke(); });
        }

        public void Delete(string key, Action onComplete = null)
        {
            Bridge.storage.Delete(key, _ => { onComplete?.Invoke(); });
        }

        public void Delete(List<string> keys, Action onComplete = null)
        {
            Bridge.storage.Delete(keys, _ => { onComplete?.Invoke(); });
        }
    }
}
#endif