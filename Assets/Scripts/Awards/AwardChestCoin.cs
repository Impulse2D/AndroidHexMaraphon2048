using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace Awards
{
    public class AwardChestCoin : MonoBehaviour
    {
        [SerializeField] private float _delay = 1f;
        [SerializeField] private float _duraction = 1f;

        private Coroutine _coroutine;

        public event Action<AwardChestCoin> Released;

        public void DoMove(Vector3 targetPosition)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(CountTimeWaitMoving(targetPosition));
        }

        public void SetParrent(Transform parrent)
        {
            transform.SetParent(parrent);
        }

        private IEnumerator CountTimeWaitMoving(Vector3 targetPosition)
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(_delay);

            yield return waitForSeconds;

            transform.DOMove(targetPosition, _duraction).onComplete = ReportReleased;
        }

        private void ReportReleased()
        {
            Released?.Invoke(this);
        }
    }
}
