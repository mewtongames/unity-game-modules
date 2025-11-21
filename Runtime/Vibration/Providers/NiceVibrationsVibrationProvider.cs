#if NICE_VIBRATIONS
using System;
using MoreMountains.NiceVibrations;

namespace MewtonGames.Modules.Vibration.Providers
{
    public class NiceVibrationsVibrationProvider : IVibrationProvider
    {
        public void Vibrate(VibrationType vibrationType)
        {
            switch (vibrationType)
            {
                case VibrationType.Light:
                    MMVibrationManager.Haptic(HapticTypes.LightImpact);
                    break;

                case VibrationType.Medium:
                    MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                    break;

                case VibrationType.Heavy:
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(vibrationType), vibrationType, null);
            }
        }
    }
}
#endif