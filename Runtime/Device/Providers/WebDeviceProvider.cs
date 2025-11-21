#if UNITY_WEBGL
using Playgama;

namespace MewtonGames.Device.Providers
{
    public class WebDeviceProvider : IDeviceProvider
    {
        public DeviceType deviceType
        {
            get
            {
                switch (Bridge.device.type)
                {
                    case Playgama.Modules.Device.DeviceType.Mobile:
                        return DeviceType.Mobile;
                    case Playgama.Modules.Device.DeviceType.Tablet:
                        return DeviceType.Tablet;
                    case Playgama.Modules.Device.DeviceType.Desktop:
                        return DeviceType.Desktop;
                    case Playgama.Modules.Device.DeviceType.TV:
                        return DeviceType.TV;
                    default:
                        return DeviceType.Desktop;
                }
            }
        }
    }
}
#endif