#if ADDRESSABLES
using System;
using System.Collections.Generic;
using MewtonGames.JSON.Converters;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MewtonGames.Modules.Localization.Providers
{
    public class AddressablesLocalizationProvider : ILocalizationProvider
    {
        private readonly ILocalizationHelper _localizationHelper;
        private readonly IJSONConverter _jsonConverter;

        public AddressablesLocalizationProvider(ILocalizationHelper localizationHelper, IJSONConverter jsonConverter)
        {
            _localizationHelper = localizationHelper;
            _jsonConverter = jsonConverter;
        }

        public void LoadLocales(SystemLanguage language, SystemLanguage defaultLanguage, Action<Dictionary<string, string>> onComplete)
        {
            Addressables
                .LoadAssetAsync<TextAsset>($"locale_{_localizationHelper.GetLanguageCode(language)}")
                .Completed += handle =>
                    {
                        if (handle.Status == AsyncOperationStatus.Succeeded)
                        {
                            var locales = _jsonConverter.DeserializeObject<Dictionary<string, string>>(handle.Result.text);
                            onComplete?.Invoke(locales);
                            return;
                        }

                        if (language == defaultLanguage)
                            return;

                        LoadLocales(defaultLanguage, defaultLanguage, onComplete);
                    };
        }
    }
}
#endif