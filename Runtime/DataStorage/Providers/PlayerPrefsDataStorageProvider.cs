using System;
using System.Collections.Generic;

namespace MewtonGames.DataStorage.Providers
{
    public class PlayerPrefsDataStorageProvider : IDataStorageProvider
    {
        public void Get(string key, Action<string> onComplete)
        {
            var value = PlayerPrefsWrapper.GetString(key, null);
            onComplete?.Invoke(value);
        }

        public void Get(List<string> keys, Action<List<string>> onComplete)
        {
            var values = new List<string>(keys.Count);

            foreach (var key in keys)
            {
                var value = PlayerPrefsWrapper.GetString(key, null);
                values.Add(value);
            }

            onComplete?.Invoke(values);
        }

        public void Set(string key, string value, Action onComplete = null)
        {
            PlayerPrefsWrapper.SetString(key, value);
            PlayerPrefsWrapper.Save();
            onComplete?.Invoke();
        }

        public void Set(List<string> keys, List<string> values, Action onComplete = null)
        {
            for (var i = 0; i < keys.Count; i++)
            {
                var key = keys[i];
                var value = values[i];
                PlayerPrefsWrapper.SetString(key, value);
            }

            PlayerPrefsWrapper.Save();
            onComplete?.Invoke();
        }

        public void Delete(string key, Action onComplete = null)
        {
            PlayerPrefsWrapper.DeleteKey(key);
            PlayerPrefsWrapper.Save();
            onComplete?.Invoke();
        }

        public void Delete(List<string> keys, Action onComplete = null)
        {
            foreach (var key in keys)
            {
                PlayerPrefsWrapper.DeleteKey(key);
            }

            PlayerPrefsWrapper.Save();
            onComplete?.Invoke();
        }
    }
}