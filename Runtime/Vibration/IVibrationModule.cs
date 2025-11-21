using MewtonGames.Common;
using MewtonGames.Vibration.Providers;

namespace MewtonGames.Vibration
{
    public interface IVibrationModule : IInitializable, IVibrationProvider
    {
        bool isEnabled { get; }
        public void SetEnabled(bool value);
    }
}