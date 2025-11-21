#if CAS
using System;
using CAS;

namespace MewtonGames.Advertisement.Providers
{
    public class CASAdvertisementProvider : IAdvertisementProvider
    {
        public event Action<InterstitialState> interstitialStateChanged;
        public event Action<RewardedState> rewardedStateChanged;
        
        public bool isInitialized { get; private set; }
        public InterstitialState interstitialState { get; private set; } = InterstitialState.Closed;
        public RewardedState rewardedState { get; private set; } = RewardedState.Closed;
        
        private IMediationManager _manager;
        private IAdView _banner;


        public void Initialize(Action onComplete = null)
        {
            MobileAds.BuildManager()
                .WithCompletionListener((config) => {
                    if (config.error == null)
                    {
                        _manager = config.manager;
                        
                        _banner = _manager.GetAdView(AdSize.Banner);
                        _banner.position = AdPosition.BottomCenter;
                        _banner.Load();

                        _manager.OnInterstitialAdFailedToLoad += OnInterstitialLoadingFailed;
                        _manager.OnInterstitialAdShown += OnInterstitialOpened;
                        _manager.OnInterstitialAdFailedToShow += OnInterstitialFailed;
                        _manager.OnInterstitialAdClosed += OnInterstitialClosed;

                        _manager.OnRewardedAdFailedToLoad += OnRewardedLoadingFailed;
                        _manager.OnRewardedAdShown += OnRewardedOpened;
                        _manager.OnRewardedAdCompleted += OnRewardedCompleted;
                        _manager.OnRewardedAdFailedToShow += OnRewardedFailed;
                        _manager.OnRewardedAdClosed += OnRewardedClosed;
                    }
                    else
                    {
                        _manager = null;
                    }
            
                    isInitialized = true;
                    onComplete?.Invoke();
                })
                .Build();
        }


        public void ShowBanner()
        {
            _banner?.SetActive(true);
        }

        public void HideBanner()
        {
            _banner?.SetActive(false);
        }
        
        public void ShowInterstitial()
        {
            if (_manager == null)
            {
                SetInterstitialState(InterstitialState.Failed);
                return;
            }
            
            _manager.ShowAd(AdType.Interstitial);
        }

        public void ShowRewarded()
        {
            if (_manager == null)
            {
                SetRewardedState(RewardedState.Failed);
                return;
            }
            
            _manager.ShowAd(AdType.Rewarded);
        }
        
        
        private void OnInterstitialOpened()
        {
            SetInterstitialState(InterstitialState.Opened);
        }

        private void OnInterstitialLoadingFailed(AdError error)
        {
            OnInterstitialFailed(error.GetMessage());
        }

        private void OnInterstitialFailed(string error)
        {
            SetInterstitialState(InterstitialState.Failed);
        }

        private void OnInterstitialClosed()
        {
            SetInterstitialState(InterstitialState.Closed);
        }
        
        private void OnRewardedOpened()
        {
            SetRewardedState(RewardedState.Opened);
        }

        private void OnRewardedLoadingFailed(AdError error)
        {
            OnRewardedFailed(error.GetMessage());
        }

        private void OnRewardedFailed(string error)
        {
            SetRewardedState(RewardedState.Failed);
        }
        
        private void OnRewardedCompleted()
        {
            SetRewardedState(RewardedState.Rewarded);
        }

        private void OnRewardedClosed()
        {
            SetRewardedState(RewardedState.Closed);
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