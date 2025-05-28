using SoundsGame;
using SpawnerStars;
using Stars;
using UnityEngine;

namespace Disablers
{
    public class StarsDisabler : MonoBehaviour
    {
        private StarsPool _starsPool;
        private StarsSpawner _starsSpawner;
        private CoinsSoundPlayer _coinsSoundPlayer;

        private void OnDisable()
        {
            _starsSpawner.StarReleasedReported -= RemoveStar;
        }

        public void Construct(StarsPool starsPool, StarsSpawner starsSpawner, CoinsSoundPlayer coinsSoundPlayer)
        {
            _starsPool = starsPool;
            _starsSpawner = starsSpawner;
            _coinsSoundPlayer = coinsSoundPlayer;

            _starsSpawner.StarReleasedReported += RemoveStar;
        }

        private void RemoveStar(Star star)
        {
            _coinsSoundPlayer.PlaySound();

            _starsPool.ReturnObject(star);
        }
    }
}
