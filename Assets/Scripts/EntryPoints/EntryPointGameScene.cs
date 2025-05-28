using CompositesRoots;
using DetectorDeviceType;
using Localozation;
using PlayerInputReader;
using SaverData;
using Services;
using SpawnerHexa;
using UI;
using UnityEngine;

public class EntryPointGameScene : MonoBehaviour
{
    [SerializeField] private CompositeRootGameScene _compositeRootGameScene;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private HexaSpawner _hexaSpawner;
    [SerializeField] private StarsSpawner _starsSpawner;
    [SerializeField] private GamePointsIndicator _gamePointsIndicator;
    [SerializeField] private ScorePanelView _scorePanelView;
    [SerializeField] private PauseService _pauseService;
    [SerializeField] private LanguageDefinition _languageDefinition;
    [SerializeField] private SoundsCustomizer _soundsCustomizer;
    [SerializeField] private LearnLoader _learnLoader;
    [SerializeField] private DeviceTypeDetector _deviceTypeDetector;

    private void Start()
    {
        _compositeRootGameScene.Init();
        _deviceTypeDetector.Init();
        _pauseService.Init();
        _inputReader.Init();
        _languageDefinition.Init();
        _hexaSpawner.Init();
        _starsSpawner.Init();
        _gamePointsIndicator.Init();
        _scorePanelView.Init();
        _soundsCustomizer.Init();
        _learnLoader.Init();
    }
}
