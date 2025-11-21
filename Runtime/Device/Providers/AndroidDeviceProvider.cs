using UnityEngine;

namespace MewtonGames.Device.Providers
{
    public class AndroidDeviceProvider : IDeviceProvider
    {
        public DeviceType deviceType
        {
            get
            {
                var aspectRatio = 1f / ((float)Screen.width / Screen.height);
                return aspectRatio < ANDROID_TABLET_ASPECT_RATIO
                    ? DeviceType.Tablet
                    : DeviceType.Mobile;
            }
        }

        private const float ANDROID_TABLET_ASPECT_RATIO = 1.44f;
    }
}