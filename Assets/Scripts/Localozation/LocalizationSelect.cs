using UnityEngine;
using YG;

namespace Localozation
{
    public class LocalizationSelect : MonoBehaviour
    {
        private const string RussianCode = "ru";
        private const string EnglishCode = "en";

        public string RussianCodeLanguage => RussianCode;
        public string EnglishCodeLanguage => EnglishCode;

        public void TranslateToRussian()
        {
            SetInstallableLanguage(RussianCode);
        }

        public void TranslateToEnglish()
        {
            SetInstallableLanguage(EnglishCode);
        }

        public void SetInstallableLanguage(string languageCode)
        {
            YandexGame.SwitchLanguage(languageCode);
        }
    }
}

