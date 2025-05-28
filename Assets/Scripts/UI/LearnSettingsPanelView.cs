using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LearnSettingsPanelView : MonoBehaviour
    {
        private Button _openingButtonLearnSettingsPanel;
        private Button _closingButtonLearnSettingsPanel;
        private GameObject _learnSettingsPanel;

        private void OnDisable()
        {
            _openingButtonLearnSettingsPanel.onClick.RemoveListener(Show);
            _closingButtonLearnSettingsPanel.onClick.RemoveListener(Hide);
        }

        public void Construct(Button openingButtonLearnSettingsPanel,
            Button closingButtonLearnSettingsPanel,
            GameObject learnSettingsPanel)
        {
            _openingButtonLearnSettingsPanel = openingButtonLearnSettingsPanel;
            _closingButtonLearnSettingsPanel = closingButtonLearnSettingsPanel;
            _learnSettingsPanel = learnSettingsPanel;

            _openingButtonLearnSettingsPanel.onClick.AddListener(Show);
            _closingButtonLearnSettingsPanel.onClick.AddListener(Hide);
        }

        private void Show()
        {
            _learnSettingsPanel.gameObject.SetActive(true);
        }

        private void Hide()
        {
            _learnSettingsPanel.gameObject.SetActive(false);
        }
    }
}
