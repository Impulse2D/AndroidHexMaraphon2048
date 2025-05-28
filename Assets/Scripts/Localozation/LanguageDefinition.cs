using UnityEngine;
using YG;

namespace Localozation
{
    public class LanguageDefinition : MonoBehaviour
    {
        private LocalizationSelect _localizationSelect;

        public void Construct(LocalizationSelect localizationSelect)
        {
            _localizationSelect = localizationSelect;
        }

        public void Init()
        {
            if (YandexGame.EnvironmentData.language == _localizationSelect.RussianCodeLanguage)
            {
                _localizationSelect.TranslateToRussian();
            }
            else if (YandexGame.EnvironmentData.language == _localizationSelect.EnglishCodeLanguage)
            {
                _localizationSelect.TranslateToEnglish();
            }
            else
            {
                _localizationSelect.TranslateToEnglish();
            }
        }
    }
}