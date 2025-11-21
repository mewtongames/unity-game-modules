using System;
using System.Collections.Generic;
using System.Linq;
using MewtonGames.UI.Providers;
using MewtonGames.UI.Windows;
using UnityEngine;

namespace MewtonGames.UI
{
    public class Stage
    {
        public bool isFocused { get; private set; }
        public Transform container { get; }
        public readonly List<IWindow> windows = new();

        private readonly IWindowsProvider _windowsProvider;
        private int _currentSortingOrder;


        public Stage(Transform container, IWindowsProvider windowsProvider, int startSortingOrder)
        {
            this.container = container;
            _windowsProvider = windowsProvider;
            _currentSortingOrder = startSortingOrder;
        }

        public void Open<T>(Action<T> onComplete = null) where T : WindowBase
        {
            var topWindow = windows.LastOrDefault();
            if (topWindow is T window)
            {
                onComplete?.Invoke(window);
                return;
            }

            if (isFocused)
            {
                topWindow?.OnLostFocus();
            }
            
            _currentSortingOrder++;

            var existWindow = windows.FirstOrDefault(w => w is T);
            if (existWindow != null)
            {
                windows.Remove(existWindow);
                windows.Add(existWindow);
                
                existWindow.transform.SetAsLastSibling();
                existWindow.SetSortingOrder(_currentSortingOrder);

                if (isFocused)
                {
                    existWindow.OnFocused();
                }

                onComplete?.Invoke(existWindow as T);
                return;
            }

            _windowsProvider.Instantiate<T>(newWindow =>
            {
                newWindow.transform.SetParent(container, false);
                newWindow.transform.SetAsLastSibling();
                newWindow.SetSortingOrder(_currentSortingOrder);

                if (isFocused)
                {
                    newWindow.OnFocused();
                }

                windows.Add(newWindow);
                onComplete?.Invoke(newWindow);
            });
        }

        public List<T> Get<T>() where T : WindowBase
        {
            return windows.Where(w => w is T).Cast<T>().ToList();
        }

        public void Close(IWindow window)
        {
            DestroyWindow(window);
        }

        public void Close<T>() where T : WindowBase
        {
            DestroyWindow(windows.LastOrDefault(w => w is T));
        }

        public void CloseAll()
        {
            foreach (var window in windows)
            {
                _windowsProvider.Destroy(window);
            }

            windows.Clear();
        }

        public void OnFocused()
        {
            if (isFocused)
            {
                return;
            }

            isFocused = true;
            var topWindow = windows.LastOrDefault();
            topWindow?.OnFocused();
        }

        public void OnLostFocus()
        {
            if (!isFocused)
            {
                return;
            }

            isFocused = false;
            var topWindow = windows.LastOrDefault();
            topWindow?.OnLostFocus();
        }

        
        private void DestroyWindow(IWindow window)
        {
            if (window == null)
            {
                return;
            }

            var topWindow = windows.LastOrDefault();
            if (topWindow == window)
            {
                if (isFocused)
                {
                    window.OnLostFocus();
                }

                windows.Remove(window);
                _windowsProvider.Destroy(window);
                _currentSortingOrder--;
                
                if (isFocused)
                {
                    var nextWindow = windows.LastOrDefault();
                    nextWindow?.OnFocused();
                }

                return;
            }

            windows.Remove(window);
            _windowsProvider.Destroy(window);
        }
    }
}