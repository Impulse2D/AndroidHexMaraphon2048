using System;
using Awards;
using SpanwerAwardsUI;
using UnityEngine;

namespace Disablers
{
    public class AwardsCoinsUiDisabler : MonoBehaviour
    {
        private AwardsUiSpawner _awardsUiSpawner;
        private AwardsUiPool _awardsUiPool;

        public void Construct(AwardsUiSpawner awardsUiSpawner, AwardsUiPool awardsUiPool)
        {
            _awardsUiSpawner = awardsUiSpawner;
            _awardsUiPool = awardsUiPool;
        }

        public void Remove(AwardUiCoin awardUiCoin)
        {
            _awardsUiPool.ReturnObject(awardUiCoin);
            _awardsUiSpawner.ClearCurrentAwardUiCoin();
        }
    }
}
