#if UNITY_IOS
namespace MewtonGames.Device.Providers
{
    public class IOSDeviceProvider : IDeviceProvider
    {
        public DeviceType deviceType => UnityEngine.iOS.Device.generation.ToString().Contains("iPad") 
            ? DeviceType.Tablet 
            : DeviceType.Mobile;
    }
}
#endif