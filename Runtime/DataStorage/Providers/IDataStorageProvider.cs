using System;
using System.Collections.Generic;

namespace MewtonGames.DataStorage.Providers
{
    public interface IDataStorageProvider
    {
        public void Get(string key, Action<string> onComplete);
        public void Get(List<string> keys, Action<List<string>> onComplete);

        public void Set(string key, string value, Action onComplete = null);
        public void Set(List<string> keys, List<string> values, Action onComplete = null);
        
        public void Delete(string key, Action onComplete = null);
        public void Delete(List<string> keys, Action onComplete = null);
    }
}