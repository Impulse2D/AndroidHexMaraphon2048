using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseView : MonoBehaviour
    {
        private PauseService _pauseService;
        private GameObject _pausePanel;
        private Button _closingPausePanelButton;

        private void OnDisable()
        {
            _closingPausePanelButton.onClick.RemoveListener(Hide);
            _pauseService.FocusNotDetected -= Show;
        }

        public void Construct(PauseService pauseService,
            GameObject pausePanel,
            Button closingPausePanelButton)
        {
            _pauseService = pauseService;
            _pausePanel = pausePanel;
            _closingPausePanelButton = closingPausePanelButton;

            _closingPausePanelButton.onClick.AddListener(Hide);
            _pauseService.FocusNotDetected += Show;
        }

        private void Show()
        {
            _pausePanel.gameObject.SetActive(true);
            _pauseService.EnablePause();
        }

        private void Hide()
        {
            _pausePanel.gameObject.SetActive(false);
            _pauseService.DisablePause();
        }
    }
}
