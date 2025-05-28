using SaverData;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class LevelReloader : MonoBehaviour
    {
        private Button _resetLevelButton;
        private SaverDataGame _saverDataGame;

        private void OnDisable()
        {
            _resetLevelButton.onClick.RemoveListener(RestartLevel);
        }

        public void Construct(Button resetLevelButton,
            SaverDataGame saverDataGame)
        {
            _resetLevelButton = resetLevelButton;
            _saverDataGame = saverDataGame;

            _resetLevelButton.onClick.AddListener(RestartLevel);
        }

        private void RestartLevel()
        {
            _saverDataGame.ResetPrefsData();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}


