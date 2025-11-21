namespace MewtonGames.Device.Providers
{
    public class MockDeviceProvider : IDeviceProvider
    {
        public DeviceType deviceType { get; }

        public MockDeviceProvider(DeviceType deviceType)
        {
            this.deviceType = deviceType;
        }
    }
}