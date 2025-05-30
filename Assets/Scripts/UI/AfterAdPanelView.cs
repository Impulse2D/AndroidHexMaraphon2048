using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AfterAdPanelView : MonoBehaviour
    {
        private PauseService _pauseService;
        private GameObject _afterAdPanel;
        private Button _playingAfterAdPanelButton;

        private void OnDisable()
        {
            _playingAfterAdPanelButton.onClick.RemoveListener(Hide);
        }

        public void Construct(PauseService pauseService,
            GameObject afterAdPanel,
            Button playingAfterAdPanelButton)
        {
            _pauseService = pauseService;
            _afterAdPanel = afterAdPanel;
            _playingAfterAdPanelButton = playingAfterAdPanelButton;

            _playingAfterAdPanelButton.onClick.AddListener(Hide);
        }

        private void Hide()
        {
            _afterAdPanel.gameObject.SetActive(false);
            _pauseService.DisablePause();
        }
    }
}
