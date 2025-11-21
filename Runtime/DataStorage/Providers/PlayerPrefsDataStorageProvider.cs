using System;
using System.Collections.Generic;
using UnityEngine;

namespace MewtonGames.DataStorage.Providers
{
    public class PlayerPrefsDataStorageProvider : IDataStorageProvider
    {
        public void Get(string key, Action<string> onComplete)
        {
            var value = PlayerPrefs.GetString(key, null);
            onComplete?.Invoke(value);
        }

        public void Get(List<string> keys, Action<List<string>> onComplete)
        {
            var values = new List<string>(keys.Count);
            
            foreach (var key in keys)
            {
                var value = PlayerPrefs.GetString(key, null);
                values.Add(value);
            }
            
            onComplete?.Invoke(values);
        }

        public void Set(string key, string value, Action onComplete = null)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
            onComplete?.Invoke();
        }

        public void Set(List<string> keys, List<string> values, Action onComplete = null)
        {
            for (var i = 0; i < keys.Count; i++)
            {
                var key = keys[i];
                var value = values[i];
                PlayerPrefs.SetString(key, value);
            }

            PlayerPrefs.Save();
            onComplete?.Invoke();
        }

        public void Delete(string key, Action onComplete = null)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
            onComplete?.Invoke();
        }

        public void Delete(List<string> keys, Action onComplete = null)
        {
            foreach (var key in keys)
            {
                PlayerPrefs.DeleteKey(key);
            }
            
            PlayerPrefs.Save();
            onComplete?.Invoke();
        }
    }
}