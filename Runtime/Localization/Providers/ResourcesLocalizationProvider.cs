using System;
using System.Collections.Generic;
using MewtonGames.JSON.Converters;
using UnityEngine;

namespace MewtonGames.Localization.Providers
{
    public class ResourcesLocalizationProvider : ILocalizationProvider
    {
        private readonly string _path;
        private readonly IJSONConverter _jsonConverter;

        public ResourcesLocalizationProvider(string path, IJSONConverter jsonConverter)
        {
            _path = path;
            _jsonConverter = jsonConverter;
        }

        public void LoadLocales(SystemLanguage language, SystemLanguage defaultLanguage, Action<Dictionary<string, string>> onComplete)
        {
            var textAsset = Resources.Load<TextAsset>($"{_path}/{LocalizationHelper.GetLanguageCode(language)}");
            if (textAsset == null && language != defaultLanguage)
            {
                textAsset = Resources.Load<TextAsset>($"{_path}/{LocalizationHelper.GetLanguageCode(defaultLanguage)}");
            }

            var locales = _jsonConverter.DeserializeObject<Dictionary<string, string>>(textAsset.text);
            onComplete?.Invoke(locales);
        }
    }
}