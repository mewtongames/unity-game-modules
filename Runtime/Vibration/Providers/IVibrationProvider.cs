namespace MewtonGames.Vibration.Providers
{
    public interface IVibrationProvider
    {
        public bool isSupported { get; }
        public void Vibrate(VibrationType vibrationType);
    }
}