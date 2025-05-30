using TMPro;
using UnityEngine;

namespace Localization
{
    public class TranslatableText : MonoBehaviour
    {
        [SerializeField] private int _textID;
        [SerializeField] private Translator _translator;
        private TextMeshProUGUI _uIText;

        public int TextID => _textID;

        private void Start()
        {
            _uIText = GetComponent<TextMeshProUGUI>();
    
            _translator.AddIdText(this);
            _translator.UpdateText();
        }

        private void OnDestroy()
        {
            _translator.DeleteIdText(this);
        }

        public void SetUiText(string uIText)
        {
            _uIText.text = uIText;
        }
    }
}

