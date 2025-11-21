using System;
using System.Collections.Generic;
using UnityEngine;

namespace MewtonGames.Localization.Providers
{
    public class MockLocalizationProvider : ILocalizationProvider
    {
        public void LoadLocales(SystemLanguage language, SystemLanguage defaultLanguage, Action<Dictionary<string, string>> onComplete)
        {
            onComplete?.Invoke(new Dictionary<string, string>());
        }
    }
}