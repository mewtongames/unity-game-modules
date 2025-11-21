using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MewtonGames.DataStorage.Providers
{
    public class RawDiskDataStorageProvider : IDataStorageProvider
    {
        private readonly string _path;

        public RawDiskDataStorageProvider(string path)
        {
            _path = path;

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        public void Get(string key, Action<string> onComplete)
        {
            var path = GetFilePath(key);
            if (!File.Exists(path))
            {
                onComplete?.Invoke(null);
                return;
            }

            var value = File.ReadAllText(path);
            if (string.IsNullOrEmpty(value))
            {
                onComplete?.Invoke(null);
                return;
            }

            onComplete?.Invoke(value);
        }

        public void Get(List<string> keys, Action<List<string>> onComplete)
        {
            var values = new List<string>(keys.Count);
            
            foreach (var key in keys)
            {
                var path = GetFilePath(key);
                if (!File.Exists(path))
                {
                    values.Add(null);
                    continue;
                }
                
                var value = File.ReadAllText(path);
                if (string.IsNullOrEmpty(value))
                {
                    values.Add(null);
                    continue;
                }

                values.Add(value);
            }
            
            onComplete?.Invoke(values);
        }

        public void Set(string key, string value, Action onComplete = null)
        {
            try
            {
                File.WriteAllText(GetFilePath(key), value);
                onComplete?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                onComplete?.Invoke();
            }
        }

        public void Set(List<string> keys, List<string> values, Action onComplete = null)
        {
            for (var i = 0; i < keys.Count; i++)
            {
                var key = keys[i];
                var value = values[i];
                
                try
                {
                    File.WriteAllText(GetFilePath(key), value);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }

            onComplete?.Invoke();
        }

        public void Delete(string key, Action onComplete = null)
        {
            var path = GetFilePath(key);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            onComplete?.Invoke();
        }

        public void Delete(List<string> keys, Action onComplete = null)
        {
            foreach (var key in keys)
            {
                var path = GetFilePath(key);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            
            onComplete?.Invoke();
        }
        
        private string GetFilePath(string key)
        {
            return Path.Combine(_path, key + ".data");
        }
    }
}