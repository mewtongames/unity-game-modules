using System;
using System.Collections.Generic;
using System.Linq;
using MewtonGames.UI.Windows;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MewtonGames.UI.Providers
{
    [CreateAssetMenu(menuName = "MewtonGames/UI/LazyWindowsProvider")]
    public class LazyWindowsProvider : ScriptableObject, IWindowsProvider
    {
        [SerializeField] private List<WindowReference<WindowBase>> _windows;

        public void Instantiate<T>(Action<T> onComplete) where T : WindowBase
        {
            var type = typeof(T);
            var windowReference = _windows.FirstOrDefault(r => r.id == type.Name);
            var window = Object.Instantiate(windowReference.reference.asset) as T;
            window.name = type.Name;
            onComplete?.Invoke(window);
        }

        public void Destroy(IWindow window)
        {
            Object.Destroy(window.transform.gameObject);
        }
    }

    [Serializable]
    public class WindowReference<T> where T: MonoBehaviour
    {
        public string id;
        public LazyLoadReference<T> reference;
    }
}