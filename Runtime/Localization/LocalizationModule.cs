using System;
using System.Collections.Generic;
using MewtonGames.Localization.Providers;
using UnityEngine;

namespace MewtonGames.Localization
{
    public class LocalizationModule : ILocalizationModule
    {
        public event Action<SystemLanguage> languageChanged;

        public bool isInitialized { get; private set; }
        public SystemLanguage currentLanguage { get; private set; }
        public SystemLanguage defaultLanguage { get; }

        private readonly ILocalizationProvider _provider;
        private Dictionary<string, string> _locales;

        public LocalizationModule(ILocalizationProvider provider, SystemLanguage defaultLanguage)
        {
            _provider = provider;
            currentLanguage = this.defaultLanguage = defaultLanguage;
        }

        public LocalizationModule(ILocalizationProvider provider, SystemLanguage currentLanguage, SystemLanguage defaultLanguage)
        {
            _provider = provider;
            this.currentLanguage = currentLanguage;
            this.defaultLanguage = defaultLanguage;
        }

        public void Initialize(Action onComplete = null)
        {
            UpdateLocales(() =>
            {
                isInitialized = true;
                onComplete?.Invoke();
            });
        }

        public void SetLanguage(SystemLanguage language)
        {
            currentLanguage = language;
            UpdateLocales(() => { languageChanged?.Invoke(currentLanguage); });
        }

        public string GetLocale(string key)
        {
            return _locales.TryGetValue(key, out var value) ? value : $"{key}_key";
        }

        private void UpdateLocales(Action onComplete)
        {
            _provider.LoadLocales(currentLanguage, defaultLanguage, locales =>
            {
                _locales = locales;
                onComplete?.Invoke();
            });
        }
    }
}