using SaverData;
using Services;
using SpawnerHexa;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameOverPanelView : MonoBehaviour
    {
        private GameObject _gameOverPanel;
        private SaverDataGame _saverDataGame;
        private Button _playingAgainGameOverButton;
        private PauseService _pauseService;
        private HexaSpawner _hexaSpawner;

        private void OnDisable()
        {
            _playingAgainGameOverButton.onClick.RemoveListener(RestartLevel);
            _hexaSpawner.GameOverDetected -= ShowGameOverPanel;
        }

        public void Construct(GameObject gameOverPanel,
            SaverDataGame saverDataGame,
            Button playingAgainGmaeOverButton,
            PauseService pauseService,
            HexaSpawner hexaSpawner)
        {
            _gameOverPanel = gameOverPanel;
            _saverDataGame = saverDataGame;
            _playingAgainGameOverButton = playingAgainGmaeOverButton;
            _pauseService = pauseService;
            _hexaSpawner = hexaSpawner;

            _playingAgainGameOverButton.onClick.AddListener(RestartLevel);
            _hexaSpawner.GameOverDetected += ShowGameOverPanel;
        }

        private void ShowGameOverPanel()
        {
            _pauseService.EnablePause();
            _gameOverPanel.gameObject.SetActive(true);
        }

        private void RestartLevel()
        {
            _saverDataGame.ResetPrefsData();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
