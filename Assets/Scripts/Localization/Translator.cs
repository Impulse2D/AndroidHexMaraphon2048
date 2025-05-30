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
                
                //���������
                "Settings", //2
                "Sound", //3
                "Language", //4

                //������ ��������

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

                //������
                "Sure you want to reset your current score?", //14
                "Yes", //15
                "No" //16
            },

            {
                "������", //0
                "�������", //1

                //���������

                "���������", //2
                "����", //3
                "����", //4
                "���������� ����� � ����������� �������", //5
                "���������� ����� � ������� �������", //6
                "��������� ������� ����� � ������������� �������", //7
                "�������", //8

                //GameOver
                "�� ��������� :(", //9
                "����������� �����", //10

                                //WinPanel
                "�������! ", // 11
                "����������", // 12

                //LoadingAdPanel
                "��������...", //13

                //������
                "�������, ��� ������ �������� ������� ���������?", //14
                "��", //15
                "���" //16
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
