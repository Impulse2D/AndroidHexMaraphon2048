using System.Collections;
using Awards;
using Disablers;
using Generations;
using Services;
using SoundsGame;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GamePointsIndicator : MonoBehaviour
{
    private const string Record = nameof(Record);
    private const string QuantityStars = nameof(QuantityStars);
    private const string CurrentQuantyScore = nameof(CurrentQuantyScore);
    private const string CoinsAwardsCounter = nameof(CoinsAwardsCounter);

    [SerializeField] private float _maxQuantityStar = 100;
    [SerializeField] private float _quantityStars;

    [SerializeField] private TextMeshProUGUI _recordWinPanel;
    [SerializeField] private TextMeshProUGUI _currentScoreWinPanel;

    [SerializeField] private ScorePanelView _scorePanelView;
    [SerializeField] private AwardsCounter _awardsCounter;

    private PauseService _pauseService;
    private GameObject _winPanel;
    private WinSoundPlayer _winSoundPlayer;
    private AwardsUiSpawner _awardsUiSpawner;
    private StarsGenerator _starsGenerator;
    private GameObject _winView;
    private int _quantyAddedScore;

    private Image _imageIndicator;
    private float _currentValuePercentage;
    private Coroutine _coroutine;

    private void OnDisable()
    {
        _starsGenerator.StarsCreated -= ShowFillAmount;
        _awardsUiSpawner.AwardUiCoinDoMoveCompleted -= ShowWinPanel;
    }

    public void Construct(Image image,
        PauseService pauseService,
        GameObject winPanel,
        WinSoundPlayer winSoundPlayer,
        ScorePanelView scorePanelView,
        AwardsCounter awardsCounter,
        AwardsUiSpawner awardsUiSpawner,
        StarsGenerator starsGenerator,
        GameObject winView)
    {
        _imageIndicator = image;

        _pauseService = pauseService;
        _winPanel = winPanel;
        _winSoundPlayer = winSoundPlayer;
        _scorePanelView = scorePanelView;
        _awardsCounter = awardsCounter;
        _awardsUiSpawner = awardsUiSpawner;
        _starsGenerator = starsGenerator;
        _winView = winView;
    }

    public void Init()
    {
        TryResetFillAmount();
        TryResetQuantityStars();

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _quantyAddedScore = 5;

        _starsGenerator.StarsCreated += ShowFillAmount;
        _awardsUiSpawner.AwardUiCoinDoMoveCompleted += ShowWinPanel;
    }

    public void TryResetQuantityStars()
    {
        float minQuantityStars = 0;

        if (PlayerPrefs.HasKey(QuantityStars) == true &&
            YandexGame.savesData.record == PlayerPrefs.GetInt(Record))
        {
            _quantityStars = PlayerPrefs.GetFloat(QuantityStars);
        }
        else
        {
            _quantityStars = minQuantityStars;
        }
    }

    public void TryResetFillAmount()
    {
        float minValueFillAmount = 0f;

        if (PlayerPrefs.HasKey(QuantityStars) == true &&
            YandexGame.savesData.record == PlayerPrefs.GetInt(Record))
        {
            CalculatePercentage(PlayerPrefs.GetFloat(QuantityStars));

            _imageIndicator.fillAmount = _currentValuePercentage;
        }
        else
        {
            _imageIndicator.fillAmount = minValueFillAmount;
        }
    }

    private void ShowFillAmount()
    {
        _quantityStars += _quantyAddedScore;

        PlayerPrefs.SetFloat(QuantityStars, _quantityStars);

        CalculatePercentage(_quantityStars);
        SetValueFillAmount(_currentValuePercentage);
        TryCompletedFill();
    }

    private void TryCompletedFill()
    {
        if (_quantityStars >= _maxQuantityStar)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _imageIndicator.fillAmount = 1;

            _scorePanelView.IncreaseCurrentScore();
            _awardsCounter.SaveYandexScoresAdward();
            _awardsCounter.ResetPrefsQuantyScoresAdward();

            YandexGame.SaveProgress();

            PlayerPrefs.DeleteKey(QuantityStars);

            TryResetFillAmount();
            TryResetQuantityStars();

            Debug.Log("Пройдено!");

            _pauseService.EnablePause();
            _winPanel.gameObject.SetActive(true);

            _recordWinPanel.text = YandexGame.savesData.record.ToString();
            _currentScoreWinPanel.text = PlayerPrefs.GetInt(CurrentQuantyScore).ToString();

            _winSoundPlayer.PlaySound();

            if (PlayerPrefs.GetInt(CoinsAwardsCounter) > 0 && PlayerPrefs.HasKey(CoinsAwardsCounter) != false)
            {
                ShowAwardsUiPanel();

                _awardsCounter.ResetCounterAwardsCoins();
            }
            else
            {
                ShowWinPanel();
            }
        }
    }

    private void ShowAwardsUiPanel()
    {
        _awardsUiSpawner.Create();
    }

    private void ShowWinPanel()
    {
        _winView.gameObject.SetActive(true);
    }

    private void SetValueFillAmount(float valuePercentage)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ShiftSlowlyValueFillAmoun(valuePercentage));
    }

    private IEnumerator ShiftSlowlyValueFillAmoun(float valuePoints)
    {
        float cuurentValueFillAmount = _imageIndicator.fillAmount;
        float valueTarget = valuePoints;
        float speedFillAmount = 1f;
        float delay = 1f;

        for (float i = 0; i < delay; i += speedFillAmount * Time.deltaTime)
        {
            yield return null;

            _imageIndicator.fillAmount = Mathf.Lerp(cuurentValueFillAmount, valueTarget, i);
        }

        _imageIndicator.fillAmount = valueTarget;
    }

    private void CalculatePercentage(float valuePoints)
    {
        _currentValuePercentage = valuePoints / _maxQuantityStar;
    }
}
