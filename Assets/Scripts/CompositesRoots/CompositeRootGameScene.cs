using Awards;
using Disablers;
using Generations;
using Hexes;
using PlayerInputReader;
using SaverData;
using Services;
using SoundsGame;
using SpanwerAwardsUI;
using SpawnerAwardChest;
using SpawnerHexa;
using SpawnerStars;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace CompositesRoots
{
    public class CompositeRootGameScene : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private HexagonsMover _hexagonsMover;
        [SerializeField] private HexaSpawner _hexaSpawner;
        [SerializeField] private HexaPool _hexaPool;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private HexesDisabler _hexesDisabler;
        [SerializeField] private SaverDataGame _prefsSaver;
        [SerializeField] private StarsGenerator _starsGenerator;
        [SerializeField] private StarsSpawner _starsSpawner;
        [SerializeField] private Image _imgageTargetStars;
        [SerializeField] private StarsDisabler _starsDisabler;
        [SerializeField] private StarsPool _starsPool;
        [SerializeField] private GamePointsIndicator _gamePointsIndicator;
        [SerializeField] private GameObject _winView;
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private Image _imageIndicator;
        [SerializeField] private SaverDataGame _saverDataGame;
        [SerializeField] private ScorePanelView _scorePanelView;
        [SerializeField] private TextMeshProUGUI _textRecordQuantyScore;
        [SerializeField] private TextMeshProUGUI _textCurrentQuantyScore;
        [SerializeField] private QuestionPanelView _questionPanelView;
        [SerializeField] private Button _resetLevelButton;
        [SerializeField] private GameOverPanelView _gameOverPanelView;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private Button _playingAgainGameOverButton;
        [SerializeField] private PauseService _pauseService;
        [SerializeField] private SettingsPanelView _settingsPanelView;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private Button _openingSettingsPanelButton;
        [SerializeField] private Button _closingButtonSettingsPanel;
        [SerializeField] private PauseView _pauseView;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private Button _closingPausePanelButton;
        [SerializeField] private Button _buttonTranslateToRussian;
        [SerializeField] private Button _buttonTranslateToEnglish;
        [SerializeField] private CoinsSoundPlayer _coinsSoundPlayer;
        [SerializeField] private AudioSource _audioSourceCoinsSoundPlayer;
        [SerializeField] private AudioClip _audioClipCoinsSoundPlayer;
        [SerializeField] private HexaCollisionSoundPlayer _hexaCollisionSoundPlayer;
        [SerializeField] private AudioSource _audioSourceHexaCollisionSoundPlayer;
        [SerializeField] private AudioClip _audioClipHexaCollisionSoundPlayer;
        [SerializeField] private AwardsView _awardsView;
        [SerializeField] private Button _openAwardPanelButton;
        [SerializeField] private Button _closeAwardPanelButton;
        [SerializeField] private GameObject _awardsPanel;
        [SerializeField] private WinPanelView _winPanelView;
        [SerializeField] private TextMeshProUGUI _award2048;
        [SerializeField] private TextMeshProUGUI _award4096;
        [SerializeField] private TextMeshProUGUI _award8192;
        [SerializeField] private TextMeshProUGUI _award16384;
        [SerializeField] private TextMeshProUGUI _award32768;
        [SerializeField] private TextMeshProUGUI _award65536;
        [SerializeField] private Image _imageComplete2048;
        [SerializeField] private Image _imageComplete4096;
        [SerializeField] private Image _imageComplete8192;
        [SerializeField] private Image _imageComplete16384;
        [SerializeField] private Image _imageComplete32768;
        [SerializeField] private Image _imageComplete65536;
        [SerializeField] private Button _closeButtonWinPanel;
        [SerializeField] private WinSoundPlayer _winSoundPlayer;
        [SerializeField] private AudioSource _audioSourcewinSoundPlayer;
        [SerializeField] private AudioClip _audioClipwinSoundPlayer;
        [SerializeField] private AwardsCounter _awardsCounter;
        [SerializeField] private AwardsUiSpawner _awardsUiSpawner;
        [SerializeField] private GameObject _spawnObjectPositionAwardUi;
        [SerializeField] private GameObject _parrentAwardUi;
        [SerializeField] private GameObject _targetObjectAwardsUi;
        [SerializeField] private AwardsCoinsUiDisabler _awardsCoinsUiDisabler;
        [SerializeField] private AwardsUiPool _awardsUiPool;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AwardChestCoinSpawner _awardChestCoinSpawner;
        [SerializeField] private GameObject _targetObjectAwardChestCoin;
        [SerializeField] private AwardChestCoinDisabler _awardChestCoinDisabler;
        [SerializeField] private AwardChestCoinPool _awardChestCoinPool;
        [SerializeField] private LearnLoader _learnLoader;
        [SerializeField] private GameObject _learnPanel;
        [SerializeField] private LearnPanelView _learnPanelView;
        [SerializeField] private Button _closeButtonLearnPanel;
        [SerializeField] private GameObject _learnArm;
        [SerializeField] private LearnSettingsPanelView _learnSettingsPanelView;
        [SerializeField] private Button _openingButtonLearnSettingsPanel;
        [SerializeField] private Button _closingButtonLearnSettingsPanel;
        [SerializeField] private GameObject _learnSettingsPanel;
        [SerializeField] private GameObject _adLoadingPanel;
        [SerializeField] private GameObject _winAfterAdPanel;
        [SerializeField] private YandexMobileAdsInterstitialDemoScript _interAdMobile;
        [SerializeField] private AfterAdPanelView _afterAdPanelView;
        [SerializeField] private Button _playingAfterAdPanelButton;
        [SerializeField] private Button _rejectionButton;
        [SerializeField] private GameObject _quationPanel;
        [SerializeField] private Button _questionPanelOpeningButton;
        public void Init()
        {
            _inputReader.Construct(_pauseService);
            _hexaSpawner.Construct(_prefsSaver, _hexaCollisionSoundPlayer, _awardChestCoinSpawner);
            _hexagonsMover.Construct(_inputReader, _camera, _hexaSpawner, _saverDataGame);
            _hexesDisabler.Construct(_hexaPool, _hexaSpawner);
            _starsGenerator.Construct(_starsSpawner, _imgageTargetStars, _hexaSpawner);
            _starsDisabler.Construct(_starsPool, _starsSpawner, _coinsSoundPlayer);
            _winSoundPlayer.Construct(_pauseService, _audioSourcewinSoundPlayer, _audioClipwinSoundPlayer);
            _gamePointsIndicator.Construct(_imageIndicator,
                _pauseService,
                _winPanel,
                _winSoundPlayer,
                _scorePanelView,
                _awardsCounter,
                _awardsUiSpawner,
                _starsGenerator,
                _winView);
            _saverDataGame.Construct(_hexaSpawner, _awardsCounter);
            _scorePanelView.Construct(_textRecordQuantyScore,
                _textCurrentQuantyScore,
                _saverDataGame);
            _questionPanelView.Construct(_resetLevelButton, 
                _rejectionButton, 
                _saverDataGame,
                _pauseService,
                _quationPanel,
                _questionPanelOpeningButton);
            _gameOverPanelView.Construct(_gameOverPanel,
                _saverDataGame,
                _playingAgainGameOverButton,
                _pauseService,
                _hexaSpawner);
            _settingsPanelView.Construct(_settingsPanel,
                _openingSettingsPanelButton,
                _closingButtonSettingsPanel,
                _pauseService);
            _pauseView.Construct(_pauseService, _pausePanel, _closingPausePanelButton);
            _coinsSoundPlayer.Construct(_pauseService, _audioSourceCoinsSoundPlayer, _audioClipCoinsSoundPlayer);
            _hexaCollisionSoundPlayer.Construct(_pauseService, _audioSourceHexaCollisionSoundPlayer, _audioClipHexaCollisionSoundPlayer);
            _awardsView.Construct(_pauseService,
                _openAwardPanelButton,
                _closeAwardPanelButton,
                _awardsPanel,
                _award2048,
                _award4096,
                _award8192,
                _award16384,
                _award32768,
                _award65536,
                _imageComplete2048,
                _imageComplete4096,
                _imageComplete8192,
                _imageComplete16384,
                _imageComplete32768,
                _imageComplete65536);
            _winPanelView.Construct(_winPanel,
                _closeButtonWinPanel,
                _awardsUiSpawner,
                _awardsCoinsUiDisabler,
                _winView,
                _adLoadingPanel,
                _winAfterAdPanel,
                _interAdMobile,
                _pauseService);

            _awardsCounter.Construct(_hexaSpawner);
            _awardsUiSpawner.Construct(_spawnObjectPositionAwardUi,
                _parrentAwardUi,
                _targetObjectAwardsUi);
            _awardsCoinsUiDisabler.Construct(_awardsUiSpawner, _awardsUiPool);
            _awardChestCoinSpawner.Construct(_targetObjectAwardChestCoin);
            _awardChestCoinDisabler.Construct(_awardChestCoinSpawner, _awardChestCoinPool, _particleSystem);
            _learnLoader.Construct(_pauseService,
                _learnPanel,
                _learnArm,
                _hexagonsMover);
            _learnPanelView.Construct(_closeButtonLearnPanel, _pauseService, _learnPanel);
            _learnSettingsPanelView.Construct(_openingButtonLearnSettingsPanel,
                _closingButtonLearnSettingsPanel,
                _learnSettingsPanel);
            _afterAdPanelView.Construct(_pauseService,
                _winAfterAdPanel,
                _playingAfterAdPanelButton);
        }
    }
}
