using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LearnPanelView : MonoBehaviour
    {
        private Button _closeButtonLearnPanel;
        private GameObject _learnPanel;
        private PauseService _pauseService;

        private void OnDisable()
        {
            _closeButtonLearnPanel.onClick.RemoveListener(Hide);
        }

        public void Construct(Button closeButtonLearnPanel, 
            PauseService pauseService,
            GameObject learnPanel)
        {
            _closeButtonLearnPanel = closeButtonLearnPanel;
            _pauseService = pauseService;
            _learnPanel = learnPanel;

            _closeButtonLearnPanel.onClick.AddListener(Hide);
        }

        private void Hide()
        {
            _learnPanel.gameObject.SetActive(false);
            _pauseService.DisablePause();
        }
    }
}
