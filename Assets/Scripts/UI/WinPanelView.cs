using System.Collections;
using Awards;
using Disablers;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinPanelView : MonoBehaviour
    {
        [SerializeField] private float _timeWaitCoroutine = 2f;
        private GameObject _winPanel;
        private Button _closeButtonWinPanel;
        private AwardsUiSpawner _awardsUiSpawner;
        private AwardsCoinsUiDisabler _awardsCoinsUiDisabler;
        private GameObject _winView;
        private GameObject _adLoadingPanel;
        private GameObject _winAfterAdPanel;
        private YandexMobileAdsInterstitialDemoScript _interAdMobile;
        private PauseService _pauseService;
        private Coroutine _coroutine;
        private int _adCounter;

        private void OnDisable()
        {
            _closeButtonWinPanel.onClick.RemoveListener(TryHideWinPanel);
        }

        public void Construct(GameObject winPanel,
            Button closeButtonWinPanel,
            AwardsUiSpawner awardsUiSpawner,
            AwardsCoinsUiDisabler awardsCoinsUiDisabler,
            GameObject winView,
            GameObject adLoadingPanel,
            GameObject winAfterAdPanel,
            YandexMobileAdsInterstitialDemoScript interAdMobile,
            PauseService pauseService)
        {
            _winPanel = winPanel;
            _closeButtonWinPanel = closeButtonWinPanel;
            _awardsUiSpawner = awardsUiSpawner;
            _awardsCoinsUiDisabler = awardsCoinsUiDisabler;
            _winView = winView;
            _adLoadingPanel = adLoadingPanel;
            _winAfterAdPanel = winAfterAdPanel;
            _interAdMobile = interAdMobile;
            _pauseService = pauseService;

            _closeButtonWinPanel.onClick.AddListener(TryHideWinPanel);
        }

        private void TryHideWinPanel()
        {
            Hide();

            if (_awardsUiSpawner.CurrentAwardUiCoin != null)
            {
                _awardsCoinsUiDisabler.Remove(_awardsUiSpawner.CurrentAwardUiCoin);
            }
        }

        private void Hide()
        {
            _adCounter++;

            int multiplicityAdLevel = 2;
            int numberLevelAd = _adCounter % multiplicityAdLevel;
            int minValueNumberLevelAd = 0;

            if (numberLevelAd == minValueNumberLevelAd)
            {
                _interAdMobile.ShowInterstitial();

                _winView.gameObject.SetActive(false);
                _winPanel.gameObject.SetActive(false);

                _adLoadingPanel.gameObject.SetActive(true);

                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                _coroutine = StartCoroutine(CountTimeOpeningWinAfterAdPanel());

                _interAdMobile.RequestInterstitial();
            }
            else
            {
                _winView.gameObject.SetActive(false);
                _winPanel.gameObject.SetActive(false);

                _pauseService.DisablePause();
            }
        }

        private IEnumerator CountTimeOpeningWinAfterAdPanel()
        {
            WaitForSecondsRealtime timeWait = new WaitForSecondsRealtime(_timeWaitCoroutine);

            yield return timeWait;

            _adLoadingPanel.gameObject.SetActive(false);
            _winAfterAdPanel.gameObject.SetActive(true);
        }
    }
}
