using System;
using MewtonGames.Device.Providers;

namespace MewtonGames.Device
{
    public class DeviceModule : IDeviceModule
    {
        public bool isInitialized { get; private set; }
        public DeviceType deviceType => _provider.deviceType;

        private readonly IDeviceProvider _provider;

        public DeviceModule()
        {
#if UNITY_WEBGL
            _provider = new WebDeviceProvider();
#elif UNITY_IOS && !UNITY_EDITOR
             _provider = new IOSDeviceProvider();
#elif UNITY_ANDROID && !UNITY_EDITOR
            _provider = new AndroidDeviceProvider();
#else
            _provider = new DesktopDeviceProvider();
#endif
        }

        public DeviceModule(IDeviceProvider provider)
        {
            _provider = provider;
        }
        
        public void Initialize(Action onComplete = null)
        {
            isInitialized = true;
            onComplete?.Invoke();
        }
    }
}