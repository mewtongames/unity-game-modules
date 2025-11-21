using MewtonGames.Advertisement.Providers;

namespace MewtonGames.Advertisement
{
    public interface IAdvertisementModule : IAdvertisementProvider
    {
        public bool isAdvertisementEnabled { get; }
        public void SetAdvertisementEnabled(bool value);
    }
}