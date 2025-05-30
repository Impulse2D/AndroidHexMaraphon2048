using SaverData;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScorePanelView : MonoBehaviour
    {
        [SerializeField] private int _maxValueScore = 1000000000;

        private const string CurrentQuantyScore = nameof(CurrentQuantyScore);
        private const string Record = nameof(Record);

        private TextMeshProUGUI _textRecordQuantyScore;
        private TextMeshProUGUI _textCurrentQuantyScore;
        private SaverDataGame _saverDataGame;

        private int _currentQuantyScore;

        public void Construct(TextMeshProUGUI textRecordQuantyScore,
            TextMeshProUGUI textCurrentQuantyScore,
            SaverDataGame saverDataGame)
        {
            _textRecordQuantyScore = textRecordQuantyScore;
            _textCurrentQuantyScore = textCurrentQuantyScore;
            _saverDataGame = saverDataGame;
        }

        public void Init()
        {
            LoadCurrentScore();
            LoadRecordAndroid();
        }

        public void IncreaseCurrentScore()
        {
            _currentQuantyScore++;

            TryUpdateRecordAndroid();

            SetTextQuantyCurrentScore(_currentQuantyScore);

            PlayerPrefs.SetInt(CurrentQuantyScore, _currentQuantyScore);

            PlayerPrefs.Save();
        }

        private void TryUpdateRecordAndroid()
        {
            if (_currentQuantyScore > PlayerPrefs.GetInt(Record))
            {
                PlayerPrefs.SetInt(Record, _currentQuantyScore);
                PlayerPrefs.Save();

                SetTextRecord(PlayerPrefs.GetInt(Record));
            }
        }

        private void LoadCurrentScore()
        {
            if (PlayerPrefs.HasKey(CurrentQuantyScore) == true)
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