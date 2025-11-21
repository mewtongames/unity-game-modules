using System;
using MewtonGames.Device;
using MewtonGames.UI.Windows;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MewtonGames.UI.Providers
{
    public class ResourcesWindowsProvider : IWindowsProvider
    {
        private readonly string _path;
        private readonly IDeviceModule _deviceModule;

        public ResourcesWindowsProvider(string path, IDeviceModule deviceModule)
        {
            _path = path;
            _deviceModule = deviceModule;
        }

        public void Instantiate<T>(Action<T> onComplete) where T : WindowBase
        {
            var type = typeof(T);
            var path = $"{_path}/{type.Name}";

            var windowPrefab = Resources.Load<T>($"{path}_{_deviceModule.deviceType}");
            if (windowPrefab == null)
            {
                windowPrefab = Resources.Load<T>(path);
            }

            var window = Object.Instantiate(windowPrefab);
            window.name = type.Name;

            onComplete?.Invoke(window);
        }

        public void Destroy(IWindow window)
        {
            Object.Destroy(window.transform.gameObject);
        }
    }
}