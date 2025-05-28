using System;
using Awards;
using Spawners;
using UnityEngine;

namespace SpawnerAwardChest
{
    public class AwardChestCoinSpawner : Spawner<AwardChestCoinPool>
    {
        private GameObject _targetObject;

        public event Action<AwardChestCoin> AwardChestCoinDoMoveCompleted;

        public void Construct(GameObject targetObject)
        {
            _targetObject = targetObject;
        }

        public AwardChestCoin Create(Vector3 spawnPosition)
        {
            Vector3 tarhetPosition = new Vector3(_targetObject.transform.position.x,
                _targetObject.transform.position.y,
                _targetObject.transform.position.z);

            AwardChestCoin awardChestCoin = ObjectsPool.GetObject(spawnPosition, Quaternion.Euler(new Vector3(90f, 0f, 0f)));

            awardChestCoin.Released += ReportRelesingAwardCoin;

            awardChestCoin.DoMove(tarhetPosition);

            return awardChestCoin;
        }

        private void ReportRelesingAwardCoin(AwardChestCoin awardChestCoin)
        {
            awardChestCoin.Released -= ReportRelesingAwardCoin;

            AwardChestCoinDoMoveCompleted?.Invoke(awardChestCoin);
        }
    }
}