using System;
using System.Collections.Generic;
using System.Linq;
using MewtonGames.UI.Providers;
using MewtonGames.UI.Windows;
using UnityEngine;

namespace MewtonGames.UI
{
    public class UIModule : MonoBehaviour, IUIModule
    {
        public bool isInitialized { get; private set; }

        private readonly Dictionary<int, Stage> _stages = new();
        private IWindowsProvider _windowsProvider;
        private int _maxStageIndex;


        public void Initialize(IWindowsProvider windowsProvider, Action onComplete = null)
        {
            if (isInitialized)
            {
                onComplete?.Invoke();
                return;
            }
            
            _windowsProvider = windowsProvider;
            isInitialized = true;
            onComplete?.Invoke();
        }

        public void Open<T>(int stageIndex = 0, Action<T> onComplete = null) where T : WindowBase
        {
            if (stageIndex > _maxStageIndex)
            {
                _maxStageIndex = stageIndex;
            }

            for (var i = 0; i < stageIndex; i++)
            {
                if (_stages.TryGetValue(i, out var previousStage))
                {
                    previousStage.OnLostFocus();
                }
            }

            var stage = GetStage(stageIndex);

            if (stageIndex >= _maxStageIndex)
            {
                stage.OnFocused();
            }

            stage.Open(onComplete);
        }

        public List<T> Get<T>(int stageIndex = 0) where T : WindowBase
        {
            var stage = GetStage(stageIndex);
            return stage.Get<T>();
        }

        public List<T> GetFromAllStages<T>() where T : WindowBase
        {
            var list = new List<T>();

            foreach (var keyValuePair in _stages)
            {
                list.AddRange(keyValuePair.Value.Get<T>());
            }

            return list;
        }

        public void Close(IWindow window)
        {
            foreach (var keyValuePair in _stages)
            {
                keyValuePair.Value.Close(window);
            }

            RemoveEmptyStages();
            UpdateFocus();
        }

        public void Close<T>(int stageIndex = 0) where T : WindowBase
        {
            var stage = GetStage(stageIndex);
            stage.Close<T>();

            RemoveEmptyStages();
            UpdateFocus();
        }

        public void CloseInAllStages<T>() where T : WindowBase
        {
            foreach (var keyValuePair in _stages)
            {
                keyValuePair.Value.Close<T>();
            }

            RemoveEmptyStages();
            UpdateFocus();
        }

        public void CloseAll(int stageIndex = 0)
        {
            var stage = GetStage(stageIndex);
            stage.CloseAll();

            RemoveEmptyStages();
            UpdateFocus();
        }

        public void CloseAllInAllStages()
        {
            foreach (var keyValuePair in _stages)
            {
                keyValuePair.Value.CloseAll();
            }

            RemoveEmptyStages();
            UpdateFocus();
        }


        private Stage GetStage(int index = 0)
        {
            _stages.TryGetValue(index, out var stage);

            if (stage == null)
            {
                var container = new GameObject($"Stage {index}").transform;
                container.SetParent(transform, false);

                stage = new Stage(container, _windowsProvider, index * 100);
                _stages.Add(index, stage);

                foreach (var keyValuePair in _stages.OrderBy(pair => pair.Key))
                {
                    keyValuePair.Value.container.SetSiblingIndex(keyValuePair.Key);
                }
            }

            return stage;
        }

        private void RemoveEmptyStages()
        {
            var maxStageIndex = 0;
            
            foreach (var keyValuePair in _stages.ToList())
            {
                if (keyValuePair.Value.windows.Count <= 0)
                {
                    Destroy(keyValuePair.Value.container.gameObject);
                    _stages.Remove(keyValuePair.Key);
                }
                else if (keyValuePair.Key > maxStageIndex)
                {
                    maxStageIndex = keyValuePair.Key;
                }
            }

            _maxStageIndex = maxStageIndex;
        }

        private void UpdateFocus()
        {
            var stages = _stages.ToList();

            if (stages.Count <= 0)
            {
                return;
            }

            for (var i = 0; i < stages.Count - 1; i++)
            {
                var keyValuePair = stages[i];
                keyValuePair.Value.OnLostFocus();
            }

            var lastStage = stages.Last();
            lastStage.Value.OnFocused();
        }
    }
}