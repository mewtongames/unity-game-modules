using System;
using MewtonGames.Common;
using UnityEngine;

namespace MewtonGames.Localization
{
    public interface ILocalizationModule : IInitializable
    {
        public event Action<SystemLanguage> languageChanged;

        public SystemLanguage currentLanguage { get; }
        public SystemLanguage defaultLanguage { get; }

        public void SetLanguage(SystemLanguage language);
        public string GetLocale(string key);
    }
}