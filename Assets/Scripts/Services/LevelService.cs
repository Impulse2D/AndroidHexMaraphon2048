using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Services
{
    public class LevelService : MonoBehaviour
    {
        private const string GameScene = nameof(GameScene);
        private const string CurrentLevel = nameof(CurrentLevel);
        private const string MainScene = nameof(MainScene);

        private int _numberLevel;

        public event Action<string> LeveliInstalled;

        public int NumberLevel => _numberLevel;

        public void Init()
        {
            LoadData();
        }

        public void LoadData()
        {
         //  _numberLevel = YandexGame.savesData.numberLevel;

            PlayerPrefs.SetInt(CurrentLevel, _numberLevel);
            Debug.Log(_numberLevel);
            LeveliInstalled?.Invoke(_numberLevel.ToString());
        }

        public void SaveData()
        {
          //  YandexGame.savesData.numberLevel = _numberLevel;
            YandexGame.SaveProgress();
        }

        public void IncreaseLevel()
        {
            _numberLevel++;
        }

        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // это для последнего уровня

        public void ResetLevel()
        {
            _numberLevel = 1;

         //   YandexGame.savesData.numberLevel = _numberLevel;

            YandexGame.SaveProgress();

            SetScene(GameScene);
        }

        public void LoadMainScene()
        {
            IncreaseLevel();
            SaveData();
            SetScene(MainScene);
        }

        public void LoadGameScene()
        {
            SetScene(GameScene);
        }

        private void SetScene(string nameScene)
        {
            SceneManager.LoadScene(nameScene);
        }
    }
}
