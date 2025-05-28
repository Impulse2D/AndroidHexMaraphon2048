using Awards;
using Disablers;
using Services;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI
{
    public class WinPanelView : MonoBehaviour
    {
        private PauseService _pauseService;
        private GameObject _winPanel;
        private Button _closeButtonWinPanel;
        private AwardsUiSpawner _awardsUiSpawner;
        private AwardsCoinsUiDisabler _awardsCoinsUiDisabler;
        private GameObject _winView;

        private void OnDisable()
        {
            YandexGame.CloseFullAdEvent -= Hide;
            _closeButtonWinPanel.onClick.RemoveListener(TryHideWinPanel);
        }

        public void Construct(PauseService pauseService,
            GameObject winPanel,
            Button closeButtonWinPanel,
            AwardsUiSpawner awardsUiSpawner,
            AwardsCoinsUiDisabler awardsCoinsUiDisabler,
            GameObject winView)
        {
            _pauseService = pauseService;
            _winPanel = winPanel;
            _closeButtonWinPanel = closeButtonWinPanel;
            _awardsUiSpawner = awardsUiSpawner;
            _awardsCoinsUiDisabler = awardsCoinsUiDisabler;
            _winView = winView;

            YandexGame.CloseFullAdEvent += Hide;
            _closeButtonWinPanel.onClick.AddListener(TryHideWinPanel);
        }

        private void TryHideWinPanel()
        {
            int maxValueTimerShowAd = 60;

            if (YandexGame.timerShowAd > maxValueTimerShowAd)
            {
                YandexGame.FullscreenShow();
            }
            else
            {
                Hide();
            }

            if (_awardsUiSpawner.CurrentAwardUiCoin != null)
            {
                _awardsCoinsUiDisabler.Remove(_awardsUiSpawner.CurrentAwardUiCoin);
            }
        }

        private void Hide()
        {
            _winView.gameObject.SetActive(false);
            _winPanel.gameObject.SetActive(false);
            _pauseService.DisablePause();
        }
    }
}
