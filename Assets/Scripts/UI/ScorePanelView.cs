using Liderboard;
using SaverData;
using TMPro;
using UnityEngine;
using YG;

namespace UI
{
    public class ScorePanelView : MonoBehaviour
    {
        [SerializeField] private int _maxValueScore = 1000000000;

        private const string CurrentQuantyScore = nameof(CurrentQuantyScore);
        private const string Record = nameof(Record);

        private TextMeshProUGUI _textRecordQuantyScore;
        private TextMeshProUGUI _textCurrentQuantyScore;
        private GamePointsIndicator _gamePointsIndicator;
        private LiderboardSaver _landboardSaver;
        private SaverDataGame _saverDataGame;

        private int _currentQuantyScore;

        public void Construct(GamePointsIndicator gamePointsIndicator,
            TextMeshProUGUI textRecordQuantyScore,
            TextMeshProUGUI textCurrentQuantyScore,
            LiderboardSaver liderboardSaver,
            SaverDataGame saverDataGame)
        {
            _gamePointsIndicator = gamePointsIndicator;
            _textRecordQuantyScore = textRecordQuantyScore;
            _textCurrentQuantyScore = textCurrentQuantyScore;
            _landboardSaver = liderboardSaver;
            _saverDataGame = saverDataGame;
        }

        public void Init()
        {
            LoadCurrentScore();

            LoadRecordYandexGames();
            //LoadRecordAndroid();
        }

        public void IncreaseCurrentScore()
        {
            _currentQuantyScore++;

            TryUpdateRecordYandexGames();

            // TryUpdateRecordAndroid();

            SetTextQuantyCurrentScore(_currentQuantyScore);

            PlayerPrefs.SetInt(CurrentQuantyScore, _currentQuantyScore);

            PlayerPrefs.Save();
        }

        private void TryUpdateRecordYandexGames()
        {
            if (_currentQuantyScore > YandexGame.savesData.record)
            {
                YandexGame.savesData.record = _currentQuantyScore;

                PlayerPrefs.SetInt(Record, _currentQuantyScore);
                PlayerPrefs.Save();

                _landboardSaver.AddNewLeaderboardScores(YandexGame.savesData.record);

                SetTextRecord(YandexGame.savesData.record);
            }
        }

        private void TryUpdateRecordAndroid()
        {
            if (_currentQuantyScore > PlayerPrefs.GetInt(Record))
            {
                PlayerPrefs.SetInt(Record, _currentQuantyScore);

                SetTextRecord(PlayerPrefs.GetInt(Record));
            }
        }

        private void LoadCurrentScore()
        {
            if (PlayerPrefs.HasKey(CurrentQuantyScore) == true &&
                YandexGame.savesData.record == PlayerPrefs.GetInt(Record))
            {
                _currentQuantyScore = PlayerPrefs.GetInt(CurrentQuantyScore);
                SetTextQuantyCurrentScore(_currentQuantyScore);
            }
            else
            {
                ResetCurrentQuantyScore();
                SetTextQuantyCurrentScore(_currentQuantyScore);
            }
        }

        private void LoadRecordYandexGames()
        {
            if (_currentQuantyScore > YandexGame.savesData.record ||
                YandexGame.savesData.record != PlayerPrefs.GetInt(Record))
            {
                _saverDataGame.ResetPrefsData();

                ResetCurrentQuantyScore();
                SetTextQuantyCurrentScore(_currentQuantyScore);

                SetTextRecord(YandexGame.savesData.record);

                PlayerPrefs.SetInt(Record, YandexGame.savesData.record);
            }
            else
            {
                SetTextRecord(YandexGame.savesData.record);

                _saverDataGame.Init();
            }
        }

        private void LoadRecordAndroid()
        {
            _saverDataGame.Init();

            if (PlayerPrefs.HasKey(Record) == true)
            {
                SetTextRecord(PlayerPrefs.GetInt(Record));
            }
            else
            {
                ResetCurrentQuantyScore();
            }
        }

        private void SetTextQuantyCurrentScore(int currentScore)
        {
            if (currentScore > _maxValueScore)
            {
                _textCurrentQuantyScore.text = _maxValueScore.ToString();
            }
            else
            {
                _textCurrentQuantyScore.text = currentScore.ToString();
            }
        }

        private void SetTextRecord(int currentScore)
        {
            if (currentScore > _maxValueScore)
            {
                _textRecordQuantyScore.text = _maxValueScore.ToString();
            }
            else
            {
                _textRecordQuantyScore.text = currentScore.ToString();
            }
        }

        private void ResetCurrentQuantyScore()
        {
            _currentQuantyScore = 0;
        }
    }
}