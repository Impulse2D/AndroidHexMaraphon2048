using Hexes;
using Services;
using UnityEngine;

public class LearnLoader : MonoBehaviour
{
    private const string StartLearnStatus = nameof(StartLearnStatus);
    private const string StartLearnFirstStatus = nameof(StartLearnFirstStatus);
    private const string StartLearnSecondStatus = nameof(StartLearnSecondStatus);

    private PauseService _pauseService;
    private GameObject _learnPanel;
    private GameObject _learnArm;
    private HexagonsMover _hexagonsMover;

    private void OnDisable()
    {
        _hexagonsMover.HexaGoingDetected -= HideLearnPointers;
    }

    public void Construct(PauseService pauseService,
        GameObject learnPanel,
        GameObject learnArm,
        HexagonsMover hexagonsMover)
    {
        _pauseService = pauseService;
        _learnPanel = learnPanel;
        _learnArm = learnArm;
        _hexagonsMover = hexagonsMover;

        _hexagonsMover.HexaGoingDetected += HideLearnPointers;
    }

    public void Init()
    {
        if (PlayerPrefs.HasKey(StartLearnStatus) == false)
        {
            _pauseService.EnablePause();
            _learnPanel.gameObject.SetActive(true);

            PlayerPrefs.SetString(StartLearnStatus, StartLearnFirstStatus);
            PlayerPrefs.Save();

            _learnArm.gameObject.SetActive(true);
        }
    }

    private void HideLearnPointers()
    {
        _learnArm.gameObject.SetActive(false);

        PlayerPrefs.SetString(StartLearnStatus, StartLearnSecondStatus);
        PlayerPrefs.Save();
    }
}
