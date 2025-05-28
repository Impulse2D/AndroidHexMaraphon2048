using System;
using SpanwerAwardsUI;
using Spawners;
using UnityEngine;

namespace Awards
{
    public class AwardsUiSpawner : Spawner<AwardsUiPool>
    {
        private GameObject _spawnObjectPositon;
        private GameObject _parrentAwardUi;
        private GameObject _targetObjectAwardsUi;
        private AwardUiCoin _currentAwardUiCoin;

        public event Action AwardUiCoinDoMoveCompleted;

        public AwardUiCoin CurrentAwardUiCoin => _currentAwardUiCoin;

        public void Construct(GameObject targetObject, 
            GameObject parrentAwardUi, 
            GameObject targetObjectAwardsUi)
        {
            _spawnObjectPositon = targetObject;
            _parrentAwardUi = parrentAwardUi;
            _targetObjectAwardsUi = targetObjectAwardsUi;
        }

        public AwardUiCoin Create()
        {
            Vector3 targetPosition = new Vector3(_targetObjectAwardsUi.transform.position.x,
                _targetObjectAwardsUi.transform.position.y,
                _targetObjectAwardsUi.transform.position.z);

            AwardUiCoin awardUiCoin = ObjectsPool.GetObject(_spawnObjectPositon.transform.position, Quaternion.identity);

            awardUiCoin.Released += ReportRelesingAwardCoin;

            awardUiCoin.SetParrent(_parrentAwardUi.transform);

            awardUiCoin.DoMove(targetPosition);

            _currentAwardUiCoin = awardUiCoin;

            return awardUiCoin;
        }

        public void ClearCurrentAwardUiCoin()
        {
            _currentAwardUiCoin = null;
        }

        private void ReportRelesingAwardCoin(AwardUiCoin awardUiCoin)
        {
            awardUiCoin.Released -= ReportRelesingAwardCoin;

            AwardUiCoinDoMoveCompleted?.Invoke();
        }
    }
}
