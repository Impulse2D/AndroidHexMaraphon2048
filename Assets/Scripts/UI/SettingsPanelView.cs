using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsPanelView : MonoBehaviour
    {
        private GameObject _settingsPanel;
        private Button _openingSettingsPanelButton;
        private Button _closingButtonSettingsPanel;
        private PauseService _pausedService;

        private void OnDisable()
        {
            _openingSettingsPanelButton.onClick.RemoveListener(Show);
            _closingButtonSettingsPanel.onClick.RemoveListener(Hide);
        }

        public void Construct(GameObject settingsPanel,
            Button openingSettingsPanelButton,
            Button closingButtonSettingsPanel,
            PauseService pausedService)
        {
            _settingsPanel = settingsPanel;
            _openingSettingsPanelButton = openingSettingsPanelButton;
            _closingButtonSettingsPanel = closingButtonSettingsPanel;
            _pausedService = pausedService;

            _openingSettingsPanelButton.onClick.AddListener(Show);
            _closingButtonSettingsPanel.onClick.AddListener(Hide);
        }

        private void Show()
        {
            _settingsPanel.SetActive(true);
            _pausedService.EnablePause();
        }

        private void Hide()
        {
            _settingsPanel.SetActive(false);
            _pausedService.DisablePause();
        }
    }
}
