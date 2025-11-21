using MewtonGames.Common;
using TMPro;
using UnityEngine;

namespace MewtonGames.Localization.Components
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private TMP_Text _text;

        private ILocalizationModule _localizationModule;

        private void Start()
        {
            _localizationModule = GameContext.Get<ILocalizationModule>();
            if (_localizationModule == null)
            {
                return;
            }

            _localizationModule.languageChanged += OnLanguageChanged;
            OnLanguageChanged(_localizationModule.currentLanguage);
        }

        private void OnDestroy()
        {
            if (_localizationModule != null)
            {
                _localizationModule.languageChanged -= OnLanguageChanged;
            }
        }

        private void OnLanguageChanged(SystemLanguage language)
        {
            _text.text = _localizationModule.GetLocale(_key);
        }
    }
}