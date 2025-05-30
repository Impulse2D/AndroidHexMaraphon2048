using SaverData;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class QuestionPanelView : MonoBehaviour
    {
        private Button _resetLevelButton;
        private Button _rejectionButton;
        private SaverDataGame _saverDataGame;
        private PauseService _pauseService;
        private GameObject _quationPanel;
        private Button _questionPanelOpeningButton;

        private void OnDisable()
        {
            _resetLevelButton.onClick.RemoveListener(RestartLevel);
            _rejectionButton.onClick.RemoveListener(Hide);
            _questionPanelOpeningButton.onClick.RemoveListener(Show);
        }

        public void Construct(Button resetLevelButton,
            Button rejectionButton,
            SaverDataGame saverDataGame,
            PauseService pauseService,
            GameObject quationPanel,
            Button questionPanelOpeningButton)
        {
            _resetLevelButton = resetLevelButton;
            _saverDataGame = saverDataGame;
            _rejectionButton = rejectionButton;
            _pauseService = pauseService;
            _quationPanel = quationPanel;
            _questionPanelOpeningButton = questionPanelOpeningButton;

            _resetLevelButton.onClick.AddListener(RestartLevel);
            _rejectionButton.onClick.AddListener(Hide);
            _questionPanelOpeningButton.onClick.AddListener(Show);
        }

        private void RestartLevel()
        {
            _saverDataGame.ResetPrefsData();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Show()
        {
            _pauseService.EnablePause();
            _quationPanel.gameObject.SetActive(true);
        }

        private void Hide()
        {
            _quationPanel.gameObject.SetActive(false);

            _pauseService.DisablePause();
        }
    }
}


