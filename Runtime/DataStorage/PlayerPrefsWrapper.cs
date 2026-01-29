using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MewtonGames.DataStorage
{
    public static class PlayerPrefsWrapper
    {
        private const string _keysFileName = "PlayerPrefsKeys";
        private const string _keysFileAssetPath = "Assets/PlayerPrefsKeys.txt";

        private static HashSet<string> _registeredKeys;


        public static string GetString(string key, string defaultValue = null)
        {
            RegisterKey(key);
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            RegisterKey(key);
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public static float GetFloat(string key, float defaultValue = 0f)
        {
            RegisterKey(key);
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        public static void SetString(string key, string value)
        {
            RegisterKey(key);
            PlayerPrefs.SetString(key, value);
        }

        public static void SetInt(string key, int value)
        {
            RegisterKey(key);
            PlayerPrefs.SetInt(key, value);
        }

        public static void SetFloat(string key, float value)
        {
            RegisterKey(key);
            PlayerPrefs.SetFloat(key, value);
        }

        public static void DeleteKey(string key)
        {
            RegisterKey(key);
            PlayerPrefs.DeleteKey(key);
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        public static void Save()
        {
            PlayerPrefs.Save();
        }

        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }


        public static List<string> GetRegisteredKeys()
        {
            LoadKeys();
            return new List<string>(_registeredKeys);
        }

        public static void AddKey(string key)
        {
            RegisterKey(key);
        }

        public static void RemoveKey(string key)
        {
            LoadKeys();
            if (_registeredKeys.Remove(key))
            {
                SaveKeysToFile();
            }
        }


        private static void RegisterKey(string key)
        {
            LoadKeys();

            if (_registeredKeys.Add(key))
            {
                SaveKeysToFile();
            }
        }

        private static void LoadKeys()
        {
            if (_registeredKeys != null)
            {
                return;
            }

            _registeredKeys = new HashSet<string>();

            var textAsset = Resources.Load<TextAsset>(_keysFileName);
            if (textAsset == null)
            {
                return;
            }

            var lines = textAsset.text.Split('\n');
            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                if (!string.IsNullOrEmpty(trimmedLine))
                {
                    _registeredKeys.Add(trimmedLine);
                }
            }
        }

        private static void SaveKeysToFile()
        {
#if UNITY_EDITOR
            var directory = System.IO.Path.GetDirectoryName(_keysFileAssetPath);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            var sortedKeys = new List<string>(_registeredKeys);
            sortedKeys.Sort();
            var content = string.Join("\n", sortedKeys);
            System.IO.File.WriteAllText(_keysFileAssetPath, content);
            AssetDatabase.Refresh();
#endif
        }
    }
}
