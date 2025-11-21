#if UNITY_WEBGL
using System;
using Playgama;

namespace MewtonGames.Advertisement.Providers
{
    public class WebAdvertisementProvider : IAdvertisementProvider
    {
        public event Action<InterstitialState> interstitialStateChanged;
        public event Action<RewardedState> rewardedStateChanged;

        public bool isInitialized { get; private set; }
        public InterstitialState interstitialState { get; private set; }
        public RewardedState rewardedState { get; private set; }

        
        public WebAdvertisementProvider()
        {
            interstitialState = ConvertInterstitialState(Bridge.advertisement.interstitialState);
            rewardedState = ConvertRewardedState(Bridge.advertisement.rewardedState);

            Bridge.advertisement.interstitialStateChanged += OnInterstitialStateChanged;
            Bridge.advertisement.rewardedStateChanged += OnRewardedStateChanged;

            interstitialState = ConvertInterstitialState(Bridge.advertisement.interstitialState);
            rewardedState = ConvertRewardedState(Bridge.advertisement.rewardedState);
        }

        public void Initialize(Action onComplete = null)
        {
            isInitialized = true;
            onComplete?.Invoke();
        }

        public void ShowBanner()
        {
            Bridge.advertisement.ShowBanner();
        }

        public void HideBanner()
        {
            Bridge.advertisement.HideBanner();
        }

        public void ShowInterstitial()
        {
            Bridge.advertisement.ShowInterstitial();
        }

        public void ShowRewarded()
        {
            Bridge.advertisement.ShowRewarded();
        }

        
        private void OnInterstitialStateChanged(Playgama.Modules.Advertisement.InterstitialState state)
        {
            SetInterstitialState(ConvertInterstitialState(state));
        }

        private void OnRewardedStateChanged(Playgama.Modules.Advertisement.RewardedState state)
        {
            SetRewardedState(ConvertRewardedState(state));
        }

        private InterstitialState ConvertInterstitialState(Playgama.Modules.Advertisement.InterstitialState state)
        {
            switch (state)
            {
                case Playgama.Modules.Advertisement.InterstitialState.Loading:
                    return InterstitialState.Loading;
                
                case Playgama.Modules.Advertisement.InterstitialState.Opened:
                    return InterstitialState.Opened;

                case Playgama.Modules.Advertisement.InterstitialState.Closed:
                    return InterstitialState.Closed;
                
                case Playgama.Modules.Advertisement.InterstitialState.Failed:
                    return InterstitialState.Failed;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private RewardedState ConvertRewardedState(Playgama.Modules.Advertisement.RewardedState state)
        {
            switch (state)
            {
                case Playgama.Modules.Advertisement.RewardedState.Loading:
                    return RewardedState.Loading;
                
                case Playgama.Modules.Advertisement.RewardedState.Opened:
                    return RewardedState.Opened;
                
                case Playgama.Modules.Advertisement.RewardedState.Rewarded:
                    return RewardedState.Rewarded;

                case Playgama.Modules.Advertisement.RewardedState.Closed:
                    return RewardedState.Closed;
                
                case Playgama.Modules.Advertisement.RewardedState.Failed:
                    return RewardedState.Failed;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetInterstitialState(InterstitialState state)
        {
            interstitialState = state;
            interstitialStateChanged?.Invoke(interstitialState);
        }

        private void SetRewardedState(RewardedState state)
        {
            rewardedState = state;
            rewardedStateChanged?.Invoke(rewardedState);
        }
    }
}
#endif