using System;
using System.Collections.Generic;
using System.Linq;
using MewtonGames.JSON.Converters;
using UnityEngine;

namespace MewtonGames.Localization.Providers
{
    [CreateAssetMenu(menuName = "MewtonGames/Localizations/LazyLocalizationsProvider")]
    public class LazyLocalizationProvider : ScriptableObject, ILocalizationProvider
    {
        [SerializeField] private List<LocalizationReference> _localizations;

        private IJSONConverter _jsonConverter;

        public void SetJSONConverter(IJSONConverter jsonConverter)
        {
            _jsonConverter = jsonConverter;
        }

        public void LoadLocales(SystemLanguage language, SystemLanguage defaultLanguage, Action<Dictionary<string, string>> onComplete)
        {
            var textAssetReference = _localizations.FirstOrDefault(r => r.id == LocalizationHelper.GetLanguageCode(language));
            if (textAssetReference == null && language != defaultLanguage)
            {
                textAssetReference = _localizations.FirstOrDefault(r => r.id == LocalizationHelper.GetLanguageCode(defaultLanguage));
            }

            var locales = _jsonConverter.DeserializeObject<Dictionary<string, string>>(textAssetReference.reference.asset.text);
            onComplete?.Invoke(locales);
        }
    }

    [Serializable]
    public class LocalizationReference
    {
        public string id;
        public LazyLoadReference<TextAsset> reference;
    }
}