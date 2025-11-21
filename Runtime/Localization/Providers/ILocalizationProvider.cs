using System;
using System.Collections.Generic;
using UnityEngine;

namespace MewtonGames.Localization.Providers
{
    public interface ILocalizationProvider
    {
        public void LoadLocales(SystemLanguage language, SystemLanguage defaultLanguage, Action<Dictionary<string, string>> onComplete);
    }
}