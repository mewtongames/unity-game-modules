using System;
using System.Collections.Generic;
using System.Linq;
using MewtonGames.DataStorage.Providers;
using MewtonGames.Time;

namespace MewtonGames.DataStorage
{
    public class DataStorageModule : IDataStorageModule
    {
        public bool isInitialized { get; private set; }
        
        private readonly Dictionary<string, string> _dataToSet = new();
        private readonly IDataStorageProvider _provider;
        private readonly bool _isSetRequestsQueueEnabled;
        private readonly ITimer _currentSetRequestsTimer;

        public DataStorageModule(DataStorageModuleSettings settings)
        {
            _provider = settings.provider;

            if (settings.enableSetRequestsQueue)
            {
                _isSetRequestsQueueEnabled = true;
                _currentSetRequestsTimer = settings.timeModule.Schedule(settings.minSetRequestsInterval, false, false);
                _currentSetRequestsTimer.stateChanged += OnTimerStateChanged;
            }
        }

        public void Initialize(Action onComplete = null)
        {
            isInitialized = true;
            onComplete?.Invoke();
        }

        public void Get(string key, Action<string> onComplete)
        {
            _provider.Get(key, onComplete);
        }

        public void Get(List<string> keys, Action<List<string>> onComplete)
        {
            _provider.Get(keys, onComplete);
        }

        public void Set(string key, string value, Action onComplete = null)
        {
            if (_isSetRequestsQueueEnabled)
            {
                _dataToSet[key] = value;
                TryToSetQueuedData();
                onComplete?.Invoke();
                return;
            }
            
            _provider.Set(key, value, onComplete);
        }

        public void Set(List<string> keys, List<string> values, Action onComplete = null)
        {
            if (keys.Count != values.Count)
            {
                throw new Exception($"keys.Count ({keys.Count} != values.Count ({values.Count})");
            }
            
            if (_isSetRequestsQueueEnabled)
            {
                for (var i = 0; i < keys.Count; i++)
                {
                    var key = keys[i];
                    var value = values[i];
                    _dataToSet[key] = value;
                }
                
                TryToSetQueuedData();
                onComplete?.Invoke();
                return;
            }
            
            _provider.Set(keys, values, onComplete);
        }

        public void ForceSetQueuedData()
        {
            if (_dataToSet.Count <= 0)
            {
                return;
            }
            
            var keys = _dataToSet.Keys.ToList();
            var values = _dataToSet.Values.ToList();
            
            _dataToSet.Clear();
            _provider.Set(keys, values);
            _currentSetRequestsTimer.Restart();
        }

        public void Delete(string key, Action onComplete = null)
        {
            _provider.Delete(key, onComplete);
        }

        public void Delete(List<string> keys, Action onComplete = null)
        {
            _provider.Delete(keys, onComplete);
        }


        private void TryToSetQueuedData()
        {
            if (_currentSetRequestsTimer.currentState is TimerState.Canceled or TimerState.Completed or TimerState.Scheduled)
            {
                ForceSetQueuedData();
            }
        }

        private void OnTimerStateChanged(TimerState state)
        {
            if (state == TimerState.Completed)
            {
                ForceSetQueuedData();
            }
        }
    }
}