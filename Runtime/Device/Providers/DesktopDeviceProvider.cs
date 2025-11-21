namespace MewtonGames.Device.Providers
{
    public class DesktopDeviceProvider : IDeviceProvider
    {
        public DeviceType deviceType { get; } = DeviceType.Desktop;
    }
}