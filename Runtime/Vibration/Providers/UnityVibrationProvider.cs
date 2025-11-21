#if UNITY_ANDROID || UNITY_IOS
using UnityEngine;
#endif

namespace MewtonGames.Vibration.Providers
{
    public class UnityVibrationProvider : IVibrationProvider
    {
#if UNITY_ANDROID || UNITY_IOS
        public bool isSupported => true;
#else
        public bool isSupported => false;
#endif

        public void Vibrate(VibrationType vibrationType)
        {
#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
        }
    }
}