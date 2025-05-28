using Hexes;
using SaverData;
using SpawnerHexa;
using UnityEngine;

namespace Disablers
{
    public class HexesDisabler : MonoBehaviour
    {
        [SerializeField] private SaverDataGame _saverDataGame;

        private HexaPool _hexaPool;
        private HexaSpawner _hexaSpawner;

        private void OnDisable()
        {
            _hexaSpawner.SameHexes—ollided -= RemoveHexa;
        }

        public void Construct(HexaPool hexaPool, HexaSpawner hexaSpawner)
        {
            _hexaPool = hexaPool;
            _hexaSpawner = hexaSpawner;

            _hexaSpawner.SameHexes—ollided += RemoveHexa;
        }

        private void RemoveHexa(Hexa currentHexa)
        {
            _hexaPool.ReturnObject(currentHexa);

            _saverDataGame.SavePrefsDataCells();
        }
    }
}
