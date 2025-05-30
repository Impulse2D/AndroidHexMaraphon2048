using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    public class Translator : MonoBehaviour
    {
        private int _laungageId;

        private List<TranslatableText> _translatableTexstId;
        private string[,] _lineText;

        public List<TranslatableText> ListId => _translatableTexstId;
        public int LaungageId => _laungageId;

        public void Awake()
        {
            _translatableTexstId = new List<TranslatableText>();

            _lineText = new string[,]
        {
            {
                "Record", //0
                "Current", //1
                
                //Настройки
                "Settings", //2
                "Sound", //3
                "Language", //4

                //Панель обучения

                "Connect blocks with the same numbers together", //5
                "Fill out the scale and set records", //6
                "Collect big numbers and earn rewards", //7
                "Close", //8

                //GameOver
                "You lost :(", //9,
                "Try again", //10

                //WinPanel
                "GREAT!", // 11
                "Continue", // 12

                //LoadingAdPanel
                "Loading...", //13

                //Вопрос
                "Sure you want to reset your current score?", //14
                "Yes", //15
                "No" //16
            },

            {
                "Рекорд", //0
                "Текущий", //1

                //Настройки

                "Настройки", //2
                "Звук", //3
                "Язык", //4
                "Соединяйте блоки с одинаковыми числами", //5
                "Заполняйте шкалу и ставьте рекорды", //6
                "Собирайте большие числа и зарабатывайте награды", //7
                "Закрыть", //8

                //GameOver
                "Вы проиграли :(", //9
                "Попробовать снова", //10

                                //WinPanel
                "ОТЛИЧНО! ", // 11
                "Продолжить", // 12

                //LoadingAdPanel
                "Загрузка...", //13

                //Вопрос
                "Уверены, что хотите сбросить текущий результат?", //14
                "Да", //15
                "Нет" //16
            }
            };
        }

        public void SelectLanguague(int id)
        {
            _laungageId = id;

            UpdateText();
        }

        /*   public string GetText(int textKey)
           {
               return _lineText[LaungageId, textKey];
           } */

        public void AddIdText(TranslatableText idText)
        {
            _translatableTexstId.Add(idText);
        }

        public void DeleteIdText(TranslatableText idText)
        {
            _translatableTexstId.Remove(idText);
        }

        public void UpdateText()
        {
            for (int i = 0; i < _translatableTexstId.Count; i++)
            {
                _translatableTexstId[i].SetUiText(_lineText[LaungageId, _translatableTexstId[i].TextID]);
            }
        }
    }
}
