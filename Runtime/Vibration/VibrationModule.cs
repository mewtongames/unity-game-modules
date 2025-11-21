using System;
using MewtonGames.Vibration.Providers;

namespace MewtonGames.Vibration
{
    public class VibrationModule : IVibrationModule
    {
        public bool isSupported => _provider.isSupported;
        public bool isInitialized { get; private set; }
        public bool isEnabled { get; private set; }

        private readonly IVibrationProvider _provider;

        public VibrationModule(IVibrationProvider provider)
        {
            _provider = provider;
        }

        public void Initialize(Action onComplete = null)
        {
            isInitialized = true;
            onComplete?.Invoke();
        }

        public void SetEnabled(bool value)
        {
            isEnabled = value;
        }

        public void Vibrate(VibrationType vibrationType)
        {
            if (!isEnabled)
            {
                return;
            }

            _provider.Vibrate(vibrationType);
        }
    }
}