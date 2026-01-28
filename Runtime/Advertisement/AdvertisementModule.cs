using System;
using MewtonGames.Advertisement.Providers;
using MewtonGames.Time;

namespace MewtonGames.Advertisement
{
    public class AdvertisementModule : IAdvertisementModule
    {
        public event Action<InterstitialState> interstitialStateChanged;
        public event Action<RewardedState> rewardedStateChanged;

        public bool isInitialized => _provider.isInitialized;
        public bool isAdvertisementEnabled { get; private set; } = true;
        public InterstitialState interstitialState { get; private set; }
        public RewardedState rewardedState { get; private set; }

        private readonly ITimeModule _timeModule;
        private readonly IAdvertisementProvider _provider;
        private readonly float _minDelayBetweenInterstitial;
        private ITimer _timerToNextInterstitial;

        
        public AdvertisementModule(IAdvertisementProvider provider, ITimeModule timeModule, float minDelayBetweenInterstitial = 60f)
        {
            _provider = provider;
            _provider.interstitialStateChanged += OnInterstitialStateChanged;
            _provider.rewardedStateChanged += OnRewardedStateChanged;
            _timeModule = timeModule;
            _minDelayBetweenInterstitial = minDelayBetweenInterstitial;
            
            interstitialState = _provider.interstitialState;
            rewardedState = _provider.rewardedState;
        }

        public void Initialize(Action onComplete = null)
        {
            _provider.Initialize(onComplete);
        }
        
        public void SetAdvertisementEnabled(bool value)
        {
            isAdvertisementEnabled = value;
        }
        
        public void ShowBanner()
        {
            if (!isAdvertisementEnabled)
            {
                return;
            }
            
            _provider.ShowBanner();
        }

        public void HideBanner()
        {
            _provider.HideBanner();
        }

        public void ShowInterstitial()
        {
            if (!CanShowAds())
            {
                return;
            }

            OnInterstitialStateChanged(InterstitialState.Loading);
            if (!isAdvertisementEnabled)
            {
                OnInterstitialStateChanged(InterstitialState.Failed);
                return;
            }
            
            if (_minDelayBetweenInterstitial > 0f)
            {
                if (_timerToNextInterstitial != null && _timerToNextInterstitial.currentState != TimerState.Completed)
                {
                    OnInterstitialStateChanged(InterstitialState.Failed);
                    return;
                }
            }
            
            _provider.ShowInterstitial();
        }

        public void ShowRewarded()
        {
            if (!CanShowAds())
            {
                return;
            }
            
            OnRewardedStateChanged(RewardedState.Loading);
            _provider.ShowRewarded();
        }


        private bool CanShowAds()
        {
            if (interstitialState == InterstitialState.Opened || interstitialState == InterstitialState.Loading)
            {
                return false;
            }
            
            if (rewardedState == RewardedState.Opened || rewardedState == RewardedState.Loading)
            {
                return false;
            }

            return true;
        }

        private void OnInterstitialStateChanged(InterstitialState state)
        {
            interstitialState = state;
            
            if (interstitialState == InterstitialState.Closed && _minDelayBetweenInterstitial > 0f)
            {
                _timerToNextInterstitial = _timeModule.Schedule(_minDelayBetweenInterstitial, true);
            }

            interstitialStateChanged?.Invoke(interstitialState);
        }

        private void OnRewardedStateChanged(RewardedState state)
        {
            if (rewardedState == RewardedState.Closed)
            {
                if (_timerToNextInterstitial == null)
                {
                    _timerToNextInterstitial = _timeModule.Schedule(_minDelayBetweenInterstitial, true);
                }
                else
                {
                    _timerToNextInterstitial.Restart();
                }
            }

            rewardedState = state;
            rewardedStateChanged?.Invoke(rewardedState);
        }
    }
}