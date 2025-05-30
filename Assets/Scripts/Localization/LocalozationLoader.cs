using UnityEngine;

namespace Localization
{
    public class LocalozationLoader : MonoBehaviour
    {
        // public TextMeshProUGUI _textMusic;

        private const string Language = nameof(Language);

        [SerializeField] private Translator _translator;

        public void Init()
        {
            if (PlayerPrefs.HasKey(Language) == false)
            {
                if (Application.systemLanguage == SystemLanguage.Russian)
                {
                    PlayerPrefs.SetInt(Language, 1);
                }
                else
                {
                    PlayerPrefs.SetInt(Language, 0);
                }
            }

            _translator.SelectLanguague(PlayerPrefs.GetInt(Language));
        }

        //Установлена на кнопках функция

        public void ChangeLanguage(int languageID)
        {
            PlayerPrefs.SetInt(Language, languageID);
            _translator.SelectLanguague(PlayerPrefs.GetInt(Language));
        }

        /*
  public void ShowText()
   {
       _textMusic.enabled = true;
       _textMusic.text = Translator.GetText(4);
   } */
    }
}
