using System;

namespace MewtonGames.Advertisement.Providers
{
    public class MockAdvertisementProvider : IAdvertisementProvider
    {
        public event Action<InterstitialState> interstitialStateChanged;
        public event Action<RewardedState> rewardedStateChanged;

        public bool isInitialized { get; private set; }
        public InterstitialState interstitialState { get; private set; } = InterstitialState.Closed;
        public RewardedState rewardedState { get; private set; } = RewardedState.Closed;

        public void Initialize(Action onComplete = null)
        {
            isInitialized = true;
            onComplete?.Invoke();
        }

        public void ShowBanner() { }
        public void HideBanner() { }

        public void ShowInterstitial()
        {
            interstitialState = InterstitialState.Opened;
            interstitialStateChanged?.Invoke(interstitialState);

            interstitialState = InterstitialState.Closed;
            interstitialStateChanged?.Invoke(interstitialState);
        }

        public void ShowRewarded()
        {
            rewardedState = RewardedState.Opened;
            rewardedStateChanged?.Invoke(rewardedState);

            rewardedState = RewardedState.Rewarded;
            rewardedStateChanged?.Invoke(rewardedState);

            rewardedState = RewardedState.Closed;
            rewardedStateChanged?.Invoke(rewardedState);
        }
    }
}