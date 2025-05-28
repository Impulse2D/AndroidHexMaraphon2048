using Awards;
using SpawnerAwardChest;
using UnityEngine;

namespace Disablers
{
    public class AwardChestCoinDisabler : MonoBehaviour
    {
        private AwardChestCoinSpawner _awardChestCoinSpawner;
        private AwardChestCoinPool _awardChestCoinPool;
        private ParticleSystem _particleSystem;

        private void OnDisable()
        {
            _awardChestCoinSpawner.AwardChestCoinDoMoveCompleted -= Remove;
        }

        public void Construct(AwardChestCoinSpawner awardChestCoinSpawner, 
            AwardChestCoinPool awardChestCoinPool,
            ParticleSystem particleSystem)
        {
            _awardChestCoinSpawner = awardChestCoinSpawner;
            _awardChestCoinPool = awardChestCoinPool;
            _particleSystem = particleSystem;

            _awardChestCoinSpawner.AwardChestCoinDoMoveCompleted += Remove;
        }

        private void Remove(AwardChestCoin awardChestCoin)
        {
            _awardChestCoinPool.ReturnObject(awardChestCoin);

            _particleSystem.Stop();

            _particleSystem.Play();
        }
    }
}
