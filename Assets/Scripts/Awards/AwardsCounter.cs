using SpawnerHexa;
using UnityEngine;
using YG;

namespace Awards
{
    public class AwardsCounter : MonoBehaviour
    {
        private const string QuantyAwardCoins2048 = nameof(QuantyAwardCoins2048);
        private const string QuantyAwardCoins4096 = nameof(QuantyAwardCoins4096);
        private const string QuantyAwardCoins8192 = nameof(QuantyAwardCoins8192);
        private const string QuantyAwardCoins16384 = nameof(QuantyAwardCoins16384);
        private const string QuantyAwardCoins32768 = nameof(QuantyAwardCoins32768);
        private const string QuantyAwardCoins65536 = nameof(QuantyAwardCoins65536);
        private const string CoinsAwardsCounter = nameof(CoinsAwardsCounter);


        [SerializeField] private int _quantyAwardCoins2048;
        [SerializeField] private int _quantyAwardCoins4096;
        [SerializeField] private int _quantyAwardCoins8192;
        [SerializeField] private int _quantyAwardCoins16384;
        [SerializeField] private int _quantyAwardCoins32768;
        [SerializeField] private int _quantyAwardCoins65536;

        private int _hexaId2048 = 10;
        private int _hexaId4096 = 11;
        private int _hexaId8192 = 12;
        private int _hexaId16384 = 13;
        private int _hexaId32768 = 14;
        private int _hexaId65536 = 15;

        private int _counterAwardsCoins;

        private HexaSpawner _hexaSpawner;

        public int CounterAwardsCoins => _counterAwardsCoins;

        private void OnDisable()
        {
            _hexaSpawner.CreatingNewHexaDetected -= AddAwards;
        }

        public void Construct(HexaSpawner hexaSpawner)
        {
            _hexaSpawner = hexaSpawner;

            _hexaSpawner.CreatingNewHexaDetected += AddAwards;

            LoadQuantyScoresAdwards();
        }

        public void SaveYandexScoresAdward()
        {
            YandexGame.savesData.award2048 += _quantyAwardCoins2048;
            YandexGame.savesData.award4096 += _quantyAwardCoins4096;
            YandexGame.savesData.award8192 += _quantyAwardCoins8192;
            YandexGame.savesData.award16384 += _quantyAwardCoins16384;
            YandexGame.savesData.award32768 += _quantyAwardCoins32768;
            YandexGame.savesData.award65536 += _quantyAwardCoins65536;
        }

        public void ResetPrefsQuantyScoresAdward()
        {
            int minQuantyScoresAdward = 0;

            PlayerPrefs.SetInt(QuantyAwardCoins2048, minQuantyScoresAdward);
            PlayerPrefs.SetInt(QuantyAwardCoins4096, minQuantyScoresAdward);
            PlayerPrefs.SetInt(QuantyAwardCoins8192, minQuantyScoresAdward);
            PlayerPrefs.SetInt(QuantyAwardCoins16384, minQuantyScoresAdward);
            PlayerPrefs.SetInt(QuantyAwardCoins32768, minQuantyScoresAdward);
            PlayerPrefs.SetInt(QuantyAwardCoins65536, minQuantyScoresAdward);

            PlayerPrefs.Save();

            _quantyAwardCoins2048 = minQuantyScoresAdward;
            _quantyAwardCoins4096 = minQuantyScoresAdward;
            _quantyAwardCoins8192 = minQuantyScoresAdward;
            _quantyAwardCoins16384 = minQuantyScoresAdward;
            _quantyAwardCoins32768 = minQuantyScoresAdward;
            _quantyAwardCoins65536 = minQuantyScoresAdward;
        }

        public void ResetCounterAwardsCoins()
        {
            _counterAwardsCoins = 0;

            PlayerPrefs.SetInt(CoinsAwardsCounter, _counterAwardsCoins);
        }

        private void LoadQuantyScoresAdwards()
        {
            _quantyAwardCoins2048 = PlayerPrefs.GetInt(QuantyAwardCoins2048);
            _quantyAwardCoins4096 = PlayerPrefs.GetInt(QuantyAwardCoins4096);
            _quantyAwardCoins8192 = PlayerPrefs.GetInt(QuantyAwardCoins8192);
            _quantyAwardCoins16384 = PlayerPrefs.GetInt(QuantyAwardCoins16384);
            _quantyAwardCoins32768 = PlayerPrefs.GetInt(QuantyAwardCoins32768);
            _quantyAwardCoins65536 = PlayerPrefs.GetInt(QuantyAwardCoins65536);
        }

        private void AddAwards(int idHexaSo)
        {
            if (idHexaSo == _hexaId2048)
            {
                _quantyAwardCoins2048++;

                IncrieaseCounterAwardsCoins();

                PlayerPrefs.SetInt(QuantyAwardCoins2048, _quantyAwardCoins2048);
            }

            if (idHexaSo == _hexaId4096)
            {
                _quantyAwardCoins4096++;

                IncrieaseCounterAwardsCoins();

                PlayerPrefs.SetInt(QuantyAwardCoins4096, _quantyAwardCoins4096);
            }

            if (idHexaSo == _hexaId8192)
            {
                _quantyAwardCoins8192++;

                IncrieaseCounterAwardsCoins();

                PlayerPrefs.SetInt(QuantyAwardCoins8192, _quantyAwardCoins8192);
            }

            if (idHexaSo == _hexaId16384)
            {
                _quantyAwardCoins16384++;

                IncrieaseCounterAwardsCoins();

                PlayerPrefs.SetInt(QuantyAwardCoins16384, _quantyAwardCoins16384);
            }

            if (idHexaSo == _hexaId32768)
            {
                _quantyAwardCoins32768++;

                IncrieaseCounterAwardsCoins();

                PlayerPrefs.SetInt(QuantyAwardCoins32768, _quantyAwardCoins32768);
            }

            if (idHexaSo == _hexaId65536)
            {
                _quantyAwardCoins65536++;

                IncrieaseCounterAwardsCoins();

                PlayerPrefs.SetInt(QuantyAwardCoins65536, _quantyAwardCoins65536);
            }
        }

        private void IncrieaseCounterAwardsCoins()
        {
            _counterAwardsCoins++;

            PlayerPrefs.SetInt(CoinsAwardsCounter, _counterAwardsCoins);
        }
    }
}
