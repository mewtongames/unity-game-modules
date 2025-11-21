using System;
using MewtonGames.Common;

namespace MewtonGames.Advertisement.Providers
{
    public interface IAdvertisementProvider : IInitializable
    {
        public event Action<InterstitialState> interstitialStateChanged;
        public event Action<RewardedState> rewardedStateChanged;

        public InterstitialState interstitialState { get; } 
        public RewardedState rewardedState { get; }
        
        public void ShowBanner();
        public void HideBanner();
        public void ShowInterstitial();
        public void ShowRewarded();
    }
}