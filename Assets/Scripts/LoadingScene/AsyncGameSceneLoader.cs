using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LoadingScene
{
    public class AsyncGameSceneLoader : MonoBehaviour
    {
        private const string LoadingScene = nameof(LoadingScene);
        private const string GameScene = nameof(GameScene);

        [SerializeField] private Image _imageIndicatorRewardPanel;
        [SerializeField] private TextMeshProUGUI _textLoading;

        private Coroutine _coroutine;
        private Coroutine _startingCoroutine;
        private AsyncOperation _asyncOperation;

        public void Init()
        {
            _imageIndicatorRewardPanel.fillAmount = 0;
            _textLoading.text = 0.ToString() + "%";

            if (SceneManager.GetActiveScene().name == LoadingScene)
            {
                if (_startingCoroutine != null)
                {
                    StopCoroutine(_startingCoroutine);
                }

                _startingCoroutine = StartCoroutine(EnableLoadingGameScene());
            }
        }

        private IEnumerator EnableLoadingGameScene()
        {
            float delay = 0.7f;

            WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

            yield return waitForSeconds;

            LoadGameScene();
        }

        private void LoadGameScene()
        {
            if (SceneManager.GetActiveScene().name == LoadingScene)
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                _coroutine = StartCoroutine(AsyncLoadingCounter());
            }
        }

        private IEnumerator AsyncLoadingCounter()
        {
            float loadingProgress;

            _asyncOperation = SceneManager.LoadSceneAsync(GameScene);

            while (!_asyncOperation.isDone)
            {
                loadingProgress = Mathf.Clamp01(_asyncOperation.progress / 0.9f);

                _textLoading.text = $"{(loadingProgress * 100).ToString("0")}%";

                _imageIndicatorRewardPanel.fillAmount = loadingProgress;

                yield return null;
            }
        }
    }
}
