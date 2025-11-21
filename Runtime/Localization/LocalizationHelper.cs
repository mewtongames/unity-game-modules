using System.Linq;
using UnityEngine;

namespace MewtonGames.Localization
{
    public static class LocalizationHelper
    {
        public static string GetLanguageCode(SystemLanguage language)
        {
            return language switch
            {
                SystemLanguage.French => "fr",
                SystemLanguage.German => "de",
                SystemLanguage.Russian => "ru",
                _ => "en"
            };
        }

        public static SystemLanguage ParseLanguageCode(string code)
        {
            code = code.Split('-').First();

            return code switch
            {
                "fr" => SystemLanguage.French,
                "de" => SystemLanguage.German,
                "ru" => SystemLanguage.Russian,
                _ => SystemLanguage.English
            };
        }
    }
}