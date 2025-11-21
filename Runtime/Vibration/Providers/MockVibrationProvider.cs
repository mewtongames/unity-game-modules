#if DEV_MODE && MEWTONGAMES_MODULES_LOGS
using UnityEngine;
#endif

namespace MewtonGames.Vibration.Providers
{
    public class MockVibrationProvider : IVibrationProvider
    {
        public bool isSupported => false;

        public void Vibrate(VibrationType vibrationType)
        {
#if DEV_MODE && MEWTONGAMES_MODULES_LOGS
            Debug.Log("MockVibrationProvider.Vibrate");
#endif
        }
    }
}