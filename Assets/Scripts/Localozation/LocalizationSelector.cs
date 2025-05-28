using UnityEngine;
using UnityEngine.UI;

namespace Localozation
{
    public class LocalizationSelector : MonoBehaviour
    {
        private LocalizationSelect _localizationSelect;
        private Button _buttonTranslateToRussian;
        private Button _buttonTranslateToEnglish;

        private void OnDisable()
        {
            _buttonTranslateToRussian.onClick.RemoveListener(_localizationSelect.TranslateToRussian);
            _buttonTranslateToEnglish.onClick.RemoveListener(_localizationSelect.TranslateToEnglish);
        }

        public void Construct(LocalizationSelect localizationSelect, Button buttonTranslateToRussian, Button buttonTranslateToEnglish)
        {
            _localizationSelect = localizationSelect;
            _buttonTranslateToRussian = buttonTranslateToRussian;
            _buttonTranslateToEnglish = buttonTranslateToEnglish;

            _buttonTranslateToRussian.onClick.AddListener(_localizationSelect.TranslateToRussian);
            _buttonTranslateToEnglish.onClick.AddListener(_localizationSelect.TranslateToEnglish);
        }
    }
}
