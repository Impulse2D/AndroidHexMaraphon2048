using SpawnerHexa;
using UnityEngine;

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
        }

        public void ResetCounterAwardsCoins()
        {
            _counterAwardsCoins = 0;

            PlayerPrefs.SetInt(CoinsAwardsCounter, _counterAwardsCoins);
        }

        private void AddAwards(int idHexaSo)
        {
            if (idHexaSo == _hexaId2048)
            {
                IncrieaseCounterAwardsCoins();

                int quantyAwardCoins2048 = PlayerPrefs.GetInt(QuantyAwardCoins2048) + 1;

                PlayerPrefs.SetInt(QuantyAwardCoins2048, quantyAwardCoins2048);
            }

            if (idHexaSo == _hexaId4096)
            {
                IncrieaseCounterAwardsCoins();

                int quantyAwardCoins4096 = PlayerPrefs.GetInt(QuantyAwardCoins4096) + 1;

                PlayerPrefs.SetInt(QuantyAwardCoins4096, quantyAwardCoins4096);
            }

            if (idHexaSo == _hexaId8192)
            {
                IncrieaseCounterAwardsCoins();

                int quantyAwardCoins8192 = PlayerPrefs.GetInt(QuantyAwardCoins8192) + 1;

                PlayerPrefs.SetInt(QuantyAwardCoins8192, quantyAwardCoins8192);
            }

            if (idHexaSo == _hexaId16384)
            {
                IncrieaseCounterAwardsCoins();

                int quantyAwardCoins16384 = PlayerPrefs.GetInt(QuantyAwardCoins16384) + 1;

                PlayerPrefs.SetInt(QuantyAwardCoins16384, quantyAwardCoins16384);
            }

            if (idHexaSo == _hexaId32768)
            {
                IncrieaseCounterAwardsCoins();

                int quantyAwardCoins32768 = PlayerPrefs.GetInt(QuantyAwardCoins32768) + 1;

                PlayerPrefs.SetInt(QuantyAwardCoins32768, quantyAwardCoins32768);
            }

            if (idHexaSo == _hexaId65536)
            {
                IncrieaseCounterAwardsCoins();

                int quantyAwardCoins65536 = PlayerPrefs.GetInt(QuantyAwardCoins65536) + 1;

                PlayerPrefs.SetInt(QuantyAwardCoins65536, quantyAwardCoins65536);
            }

            PlayerPrefs.Save();
        }

        private void IncrieaseCounterAwardsCoins()
        {
            _counterAwardsCoins++;

            PlayerPrefs.SetInt(CoinsAwardsCounter, _counterAwardsCoins);
        }
    }
}
